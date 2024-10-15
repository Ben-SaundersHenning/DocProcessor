using System.Data.Common;
using System.Runtime.InteropServices.JavaScript;

namespace CDP;

public class Scanner
{
    private readonly string source;
    private readonly List<Token> tokens = new List<Token>();
    private int start = 0;
    private int current = 0;
    private int line = 1;

    private static readonly Dictionary<String, TokenType> keywords = new Dictionary<string, TokenType>()
    {
        {"and",    TokenType.AND },
        {"class",  TokenType.CLASS},
        {"else",   TokenType.ELSE},
        {"false",  TokenType.FALSE},
        {"for",    TokenType.FOR},
        {"fun",    TokenType.FUN},
        {"if",     TokenType.IF},
        {"nil",    TokenType.NIL},
        {"or",     TokenType.OR},
        {"print",  TokenType.PRINT},
        {"return", TokenType.RETURN},
        {"super",  TokenType.SUPER},
        {"this",   TokenType.THIS},
        {"true",   TokenType.TRUE},
        {"var",    TokenType.VAR},
        {"while",  TokenType.WHILE}
    };

    internal Scanner(string source)
    {
        this.source = source;
    }

    public List<Token> scanTokens()
    {
        while (!isAtEnd())
        {
            start = current;
            scanToken();

        }
        
        tokens.Add(new Token(TokenType.EOF, "", null, line));

        return tokens;

    }

    private void scanToken()
    {
        char c = advance();
        switch (c)
        {
           case '(': addToken(TokenType.LEFT_PAREN); break;
           case ')': addToken(TokenType.RIGHT_PAREN); break;
           case '{': addToken(TokenType.LEFT_BRACE); break;
           case '}': addToken(TokenType.RIGHT_BRACE); break;
           case ',': addToken(TokenType.COMMA); break;
           case '.': addToken(TokenType.DOT); break;
           case '-': addToken(TokenType.MINUS); break;
           case '+': addToken(TokenType.PLUS); break;
           case ';': addToken(TokenType.SEMICOLON); break;
           case '*': addToken(TokenType.STAR); break;
           case '!':
               addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
               break;
           case '=':
               addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
               break;
           case '<':
               addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
               break;
           case '>':
               addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
               break; 
           case '/':
               if (match('/')) {
                   // A comment goes until the end of the line.
                   while (peek() != '\n' && !isAtEnd()) advance();
               } else {
                   addToken(TokenType.SLASH);
               }
               break; 
           case ' ':
           case '\r':
           case '\t':
               // Ignore whitespace.
               break;

           case '\n':
               line++;
               break;
           case '"': 
               strings();
               break;
           default:
               if (Char.IsDigit(c))
               {
                   number();
               }
               else if (Char.IsLetter(c))
               {
                   identifier();
               }
               else 
               {
                   Cdp.error(line, "Unexpected character.");
               }
               break;
        }
    }

    private void identifier()
    {
        
        while (Char.IsLetterOrDigit(peek())) advance();

        //string text = source.Substring(start, current);
        string text = source.Substring(start, current - start);
        if (keywords.ContainsKey(text))
        {
            TokenType type = keywords[text];
            addToken(type);
        }
        else
        {
            addToken(TokenType.IDENTIFIER);
        }
        
    }

    private void number()
    {
        while (Char.IsDigit(peek())) advance();

        if (peek() == '.' && Char.IsDigit(peekNext()))
        {
            advance();

            while (Char.IsDigit(peek())) advance();
        }
        
        //addToken(TokenType.NUMBER, Double.Parse(source.Substring(start, current)));
        addToken(TokenType.NUMBER, Double.Parse(source.Substring(start, current - start)));
        
    }

    private char peekNext()
    {
        if (current + 1 >= source.Length) return '\0';
        return source.ElementAt(current + 1);
    }
    
    private void strings()
    {
        while (peek() != '"' && !isAtEnd())
        {
            if (peek() == '\n') line++;
            advance();
        }

        if (isAtEnd())
        {
            Cdp.error(line, "Unterminated string.");
            return;
        }

        advance();
        
        // string value = source.Substring(start + 1, current - 1);
        string value = source.Substring(start + 1, current - start - 2);
        addToken(TokenType.STRING, value);

    }

    private char peek()
    {
        if (isAtEnd()) return '\0';
        return source.ElementAt(current);
    }

    private bool match(char expected)
    {
        if (isAtEnd()) return false;
        if (source.ElementAt(current) != expected) return false;

        current++;
        return true;
        
    }

    private char advance()
    {
        return source.ElementAt(current++);
    }

    private void addToken(TokenType type)
    {
        addToken(type, null);
    }

    private void addToken(TokenType type, Object literal)
    {
        //string text = source.Substring(start, current);
        string text = source.Substring(start, current - start);
        tokens.Add(new Token(type, text, literal, line));
    }

    private bool isAtEnd()
    {
        return current >= source.Length;
    }
    
}