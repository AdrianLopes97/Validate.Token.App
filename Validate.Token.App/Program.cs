using Validate.Token.App.Utils;

class Program
{
    static void Main()
    {
        var tokens = new List<string>() 
        { 
            "exemplo123",
            "outra_variavel",
            "nome_valido",
            "variavel_1",
            "1a",
            "blabla+",
            "blab*bla"
        };

        foreach (string token in tokens)
        {
            Lexer.Tokenize(token);
        }
    }
}
