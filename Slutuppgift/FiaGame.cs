using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift
{
    class FiaGame
    {
        public Player[] Players { get; set; }

        public Board Board { get; set; }

        public FiaGame(int boardLeftCornerX = 0, int boardLeftCornerY = 0, int numberOfPlayers = 4)
        {
            Players = CreatePlayers(numberOfPlayers);
        }

        public void StartGame()
        {
            Welcome();
            Setup();
            RunGame();
        }

        public void Welcome()
        {
            bool endLoop = false;

            Console.WriteLine("==  Welcome to Fia!  ==");


            do
            {
                Console.WriteLine("Would you like to...");
                Console.WriteLine("[1] Start new game.");
                Console.WriteLine("[2] Read rules.");
                Console.WriteLine("[3] Exit.");
                int choice = InputHelper.ReadNumber();

                switch (choice)
                {
                    case 1:
                        endLoop = true;
                        break;
                    case 2:
                        DisplayRules();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }

            } while (!endLoop);
        }

        public void Setup()
        {

        }

        public void RunGame()
        {

        }

        public void DisplayRules()
        {

        }
        

        private Player[] CreatePlayers(int numberOfPlayers)
        {
            var playerArray = new Player[numberOfPlayers];

            var colorArray = new ConsoleColor[4]
            {
                ConsoleColor.Red,
                ConsoleColor.Blue,
                ConsoleColor.Green,
                ConsoleColor.Yellow
            };

            for (int i = 0; i < numberOfPlayers; i++)
            {
                playerArray[i] = new Player(colorArray[i], i+1);
            }

            return playerArray;
        }

        public int[] BoardPositionToCoords(int boardPosition)
        {
            //int[] boardStartCoords = new int[2] { BoardLeftCorner[0], BoardLeftCorner[1] + 4 };
            int[] boardCoords = new int[2];

            if (boardPosition < 10)
            {
                if (boardPosition < 5)
                {
                    boardCoords[0] += boardPosition;
                }

                if (boardPosition > 5)
                {
                    boardCoords[1] += boardPosition - 5;
                }

                if (boardPosition > 8)
                {
                    boardCoords[0] += 1;
                }
            }

            return boardCoords;
        }

        
    }
}
