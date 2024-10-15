namespace CDP;

public class Scanner
{
    private readonly string source;
    private readonly List<Token> tokens = new List<Token>();
    private int start = 0;
    private int current = 0;
    private int line = 1;

    internal Scanner(string soruce)
    {
        this.source = source;
    }

    internal List<Token> scanTokens()
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
           default:
               Cdp.error(line, "Unexpected character.");
               break;
        }
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
        string text = source.Substring(start, current);
        tokens.Add(new Token(type, text, literal, line));
    }

    private bool isAtEnd()
    {
        return current >= source.Length;
    }
    
}