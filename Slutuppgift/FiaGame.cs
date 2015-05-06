using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                Players[i] = new Player(_defaultColors[i], i + 1, Board);
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
                            Board.DrawBoard();
                            Board.PlacePieces(Players);
                            
                            pieceToMove = InputHelper.ReadNumber() - 1;

                            if (pieceToMove < 0 || pieceToMove > 3)
                            {
                                continue;
                            }

                            if (movablePieces[pieceToMove])
                            {
                                MovePiece(player.Pieces[pieceToMove], dieRoll);
                                Shove(player.Pieces[pieceToMove]);
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
                        Console.WriteLine("Player {0} has won!!", player.PlayerNumber);
                        break;
                    } 
                }
            }
        }

        public void MovePiece(Piece piece, int dieRoll)
        {
            Board.DrawBoard();
            piece.InNest = false;
            int[] lastPosition = new int[2];
            bool backwards = false;

            for (int i = 0; i < dieRoll; i++)
            {
                if (piece.Progress == 45)
                {
                    backwards = true;
                }
                if (backwards)
                {
                    piece.Progress--;
                }
                else
                {
                    piece.Progress++;
                }
                if (i > 0 && piece.BoardPosition != 1 && !(backwards && piece.Progress == 44))
                {
                    if (backwards)
                    {
                        lastPosition = Board.BoardPositionToCoords(piece.BoardPosition + 1);
                    }
                    else if (piece.BoardPosition == 1)
                    {
                        lastPosition = Board.BoardPositionToCoords(40);
                    }
                    else
                    {
                        lastPosition = Board.BoardPositionToCoords(piece.BoardPosition - 1); 
                    }
                    Console.SetCursorPosition(lastPosition[0], lastPosition[1]);
                    Console.BackgroundColor = Board.BoardBackgroundColor;
                    Console.ForegroundColor = Board.BoardSpotsColor;
                    Console.Write("o");
                }
                Board.PlacePieces(Players);
                Thread.Sleep(500);
            }
        }

        public void Shove (Piece shover)
        {
            foreach (Player player in Players)
            {
                if (player.PlayerNumber == shover.Owner)
                {
                    continue;
                }
                foreach (Piece piece in player.Pieces)
                {
                    if (shover.BoardPosition == piece.BoardPosition)
                    {
                        piece.InNest = true;
                        piece.Progress = 0;
                        Console.ForegroundColor = player.Color;
                        Console.WriteLine("SHOVED");
                        Console.ForegroundColor = ConsoleColor.Gray;
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
    }
}
