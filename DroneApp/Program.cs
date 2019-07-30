using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DroneApp
{
    public class Program
    {
        /// <summary>
        /// CONST int NUMBER_LIMIT
        /// </summary>
        const int NUMBER_LIMIT = 2147483647;
   
        static void Main(string[] args)
        {
            Console.WriteLine("Informe a posição a ser executado  N, S, L, O seguido da posição desejada (ex: N123)");
            Console.WriteLine("Para cancelar a operação digite X");
            Console.WriteLine("Digite EXIT ou QUIT para Sair.");

            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine("Valor Lido:" + input);
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) 
                    || input.Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                var tuple = Evaluate(input.ToUpper());
                Console.WriteLine("({0},{1})", tuple.Item1, tuple.Item2);
            }
        }

        /// <summary>
        /// <param name="value">String</param>
        /// <returns>Retorna um booleano</returns>
        /// </summary>
        private static bool IsNumeric(string value)
        {
            return value.Any(x => Char.IsDigit(x));
        }

       /// <summary>
       /// Valida entrada de dados
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        private static bool ValidateInput(string input)
        {
            if (input == null) {
                return false;   
            }

            var regex = new Regex(@"[NSLOX\d]", RegexOptions.IgnoreCase);
            var matches = regex.Matches(input);
            // valida se tamanho da entrada e igual valores encontrados regex
            if (matches.Count == 0 || matches.Count != input.Length)
            {
                return false;
            }
         
            for (var i = 0; i < matches.Count; i++)
            {

                if (i == 0 && IsNumeric(matches[i].Value)) {
                    return false;
                }

                if (String.Compare(matches[i].ToString(), CoordinateEnumerable.ANULA, true) == 0)
                {
                    if (i == 0 || IsNumeric(matches[i].NextMatch().Value))
                    {   
                        return false;
                    }
                }
            }

            return true;
        }

        public static Tuple<int, int> Evaluate(string input)
        {
            int x = 0;
            int y = 0;

            //Valida argumentos de entrada, caso invalido  retorna valor 999 
            if (!ValidateInput(input) || !StepValidate(input))
            {
                return Tuple.Create(999, 999);
            }

            var formatString = input.Replace("N", "|N");
            formatString = formatString.Replace("S", "|S");
            formatString = formatString.Replace("L", "|L");
            formatString = formatString.Replace("O", "|O");
            formatString = formatString.Replace("X", "|X");

            var inputList = formatString.Split('|').ToList();

            while (inputList.FindIndex(c => c == "X") > 0)
            {
                inputList.RemoveAt(inputList.FindIndex(c => c == "X") - 1);
                inputList.RemoveAt(inputList.FindIndex(c => c == "X"));
            }

            foreach (string coordinate in inputList.ToList()) {

                int point = 0;
                // coordinate[0] == 'N'
                if (coordinate.StartsWith(CoordinateEnumerable.NORTE))
                {
                    point = coordinate.Length == 1 ? 1 : GetStep(coordinate);
                    y += point;

                }
                else if (coordinate.StartsWith(CoordinateEnumerable.SUL))
                {
                    //Console.WriteLine("SUL");
                    point = coordinate.Length == 1 ? 1 : GetStep(coordinate);
                    y -= point;

                }
                else if (coordinate.StartsWith(CoordinateEnumerable.LESTE))
                {
                    //Console.WriteLine("LESTE");
                    point = coordinate.Length == 1 ? 1 : GetStep(coordinate);
                    x += point;
                }
                else if (coordinate.StartsWith(CoordinateEnumerable.OESTE)) {
                    //Console.WriteLine("OESTE");
                    point = coordinate.Length == 1 ? 1 : GetStep(coordinate);
                    x -= point;

                }

            }
            return Tuple.Create(x, y);
        }

        /// <summary>
        /// Obtem o valor inteiro do passo
        /// </summary>
        /// <param name="stringStep"></param>
        /// <returns></returns>
        private static int GetStep(string stringStep)
        {
            return int.Parse(stringStep.Substring(1, stringStep.Length - 1));
        }

        /// <summary> 
        /// Valida o valor mínimo e máximo de passos compreendido entre 1 e 2147483647
        /// <param name="input">String</param>
        /// <returns>Retorna um booleano</returns>
        // </summary>
        private static bool StepValidate(string input)
        {
            var regex = new Regex(@"[^\d]");
            var matches = regex.Split(input);
          

            return !matches.Any(x => x.Length > 0 && int.Parse(x) < 1 && int.Parse(x) < NUMBER_LIMIT);
        }
    }
}
