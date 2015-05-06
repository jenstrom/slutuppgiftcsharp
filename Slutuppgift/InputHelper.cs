using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift
{
    static class InputHelper
    {
        public static int ReadNumber()
        {
            string input;
            int outputInt;
            string error = "";

            do
            {
                Console.WriteLine(error);
                Console.Write("> ");
                input = Console.ReadLine();

                if (input.ToLower() == "x")
                    Environment.Exit(0);

                error = "Invalid input";
            } while (!Int32.TryParse(input, out outputInt));

            return outputInt;
        }
    }
}
