using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new FiaGame();

            game.StartGame();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;

            
            Console.Read();


            
        }

        
    }
}
