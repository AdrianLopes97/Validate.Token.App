using System.Text;
using Validate.Token.App.Enums;

namespace Validate.Token.App.Utils
{
    public static class Lexer
    {
        public static void Tokenize(string input)
        {
            ELexerState state = ELexerState.Start;
            StringBuilder validToken = new();
            StringBuilder invalidToken = new();

            try
            {
                if (input.Length > 10) 
                {
                    throw LengthError(input, input.Length);
                }

                foreach (char c in input)
                {
                    state = ProcessChar(c, state, validToken, invalidToken);
                }

                if (state == ELexerState.Invalid)
                {
                    throw Error(invalidToken.ToString());
                }

                if (validToken.Length == input.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nToken: {validToken.ToString()} identificado com sucesso!");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro: {e.Message}");
                Console.WriteLine($"Token: ->{input}<- não identificado!");

                return;
            }
        }


        private static ELexerState ProcessChar(char c, ELexerState state, StringBuilder validToken, StringBuilder invalidToken)
        {
            switch (state)
            {
                case ELexerState.Start:
                    return HandleStartState(c, validToken, invalidToken);
                case ELexerState.Identifier:
                    return HandleIdentifierState(c, validToken, invalidToken);
                case ELexerState.Invalid:
                    throw Error(invalidToken.ToString());
                default:
                    throw new InvalidOperationException("Estado desconhecido do lexer.");
            }
        }
        private static ELexerState HandleStartState(char c, StringBuilder validToken, StringBuilder invalidToken)
        {
            if (char.IsLetter(c) || c == '_')
            {
                validToken.Append(c);
                return ELexerState.Identifier;
            }
            else
            {
                invalidToken.Append(c);
                return ELexerState.Invalid;
            }
        }
        private static ELexerState HandleIdentifierState(char c, StringBuilder validToken, StringBuilder invalidToken)
        {
            if (char.IsLetterOrDigit(c) || c == '_')
            {
                validToken.Append(c);
                return ELexerState.Identifier;
            }
            else
            {
                invalidToken.Append(c);
                return ELexerState.Invalid;
            }
        }
        private static Exception Error(string token)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErro léxico: '{token}' não é permitido!");
            return new Exception("Erro léxico identificado!");
        }

        private static Exception LengthError(string token, int size)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nTamanho do token '{token}' maior que o permitido {size} > 10");
            return new Exception("Erro léxico identificado!");
        }
    }
}
