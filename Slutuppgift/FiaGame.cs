using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift
{
    class FiaGame
    {
        private ConsoleColor[] _defaultColors = new ConsoleColor[4]
            {
                ConsoleColor.Red,
                ConsoleColor.Blue,
                ConsoleColor.Green,
                ConsoleColor.Yellow
            };
        public Player[] Players { get; set; }

        public Board Board { get; set; }

        public FiaGame()
        {
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
            SetupBoard();
            SetupPlayers();
        }

        public void SetupBoard()
        {
            Board = new Board();
            Board.DrawBoard();
        }

        public void SetupPlayers()
        {
            int numOfPlayers;
            do
            {
                Console.WriteLine("Please select number of players (2-4)");
                numOfPlayers = InputHelper.ReadNumber();

                if (numOfPlayers > 1 && numOfPlayers < 5)
                {
                    break;
                }
            } while (true);

            Players = new Player[numOfPlayers];

            for (int i = 0; i < numOfPlayers; i++)
            {
                Players[i] = new Player(_defaultColors[i], i + 1);
            }
        }

        public void RunGame()
        {
            int dieRoll;
            bool[] movablePieces;
            int pieceToMove;
            bool winner = false;

            Console.WriteLine("Game starting!");
            
            while (!winner)
            {
                foreach(Player player in Players)
                {
                    for (int i = 0; i < player.Pieces.Length; i++)
                    {
                        Console.WriteLine("Piece {0} progress: {1}", i + 1, player.Pieces[i].Progress);
                    }
                    Console.WriteLine("Player {0} rolls...", player.PlayerNumber);
                    dieRoll = Die.Roll();
                    Console.WriteLine("Player {0} gets a {1}", player.PlayerNumber, dieRoll);

                    movablePieces = player.GetMovablePieces(dieRoll);

                    if(movablePieces.Contains(true))
                    {
                        while (true)
                        {
                            Console.WriteLine("You can move one of the following pieces:");

                            for (int i = 0; i < movablePieces.Length; i++)
                            {
                                if (movablePieces[i])
                                {
                                    Console.WriteLine("[{0}]", i+1);
                                }
                            }
                            pieceToMove = InputHelper.ReadNumber() - 1;

                            if (movablePieces[pieceToMove])
                            {
                                player.MovePiece(pieceToMove, dieRoll);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("None of your pieces are allowed to move. How unfortunate.");
                    }

                    winner = PlayerHasWon(player);

                    if (winner)
                    {
                        break;
                    } 
                }
            }
        }

        public bool PlayerHasWon(Player player)
        {
            if (player.Pieces[0].Progress == 45
                && player.Pieces[1].Progress == 45
                && player.Pieces[2].Progress == 45
                && player.Pieces[3].Progress == 45
                )
            {
                return true;
            }
            return false;
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
