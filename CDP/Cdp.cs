namespace CDP;

public class Cdp
{
    
    static bool hadError = false;

    static void Main(string[] args)
    {
        Console.WriteLine($"ARGS LEN: {args.Length}");
        if (args.Length > 1 || args.Length == 0)
        {
            Console.WriteLine("Usage: cdp [script]");
            System.Environment.Exit(64);

        }
        else // Interpreter
        {
            runFile(args[0]);
        }
    }

    private static void runFile(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        run(System.Text.Encoding.UTF8.GetString(bytes));

        if (hadError) System.Environment.Exit(65);
    }

    private static void run(String source)
    {
        // TODO: tokenize source
        Scanner scanner = new Scanner(source);
        List<Token> tokens = scanner.scanTokens();
        foreach (Token token in tokens)
        {
            Console.WriteLine(token);
        }
    }

    internal static void error(int line, string message)
    {
        report(line, "", message);
    }

    private static void report(int line, string where, string message)
    {
        Console.Error.WriteLine($"[line {line}] Error {where}: {message}");
        hadError = true;
    }

}