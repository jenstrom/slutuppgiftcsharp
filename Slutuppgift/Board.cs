using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift
{
    class Board
    {
        public ConsoleColor BoardBackgroundColor { get; set; }
        public ConsoleColor BoardSpotsColor { get; set; }
        public string[] BoardSpotsLayout { get; private set; }
        public int[] BoardTopLeftCorner { get; set; }

        public Board(int boardTopLeftCornerX = 50, int boardTopLeftCornerY = 0, 
            ConsoleColor boardBackgroundColor = ConsoleColor.DarkGray,
            ConsoleColor boardSpotsColor = ConsoleColor.White)
        {
            BoardSpotsLayout = CreateBoardArray();
            BoardBackgroundColor = boardBackgroundColor;
            BoardSpotsColor = boardSpotsColor;
            BoardTopLeftCorner = new int[2] {boardTopLeftCornerX, boardTopLeftCornerY};
        }

        public void DrawBoard()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < Console.WindowHeight - 3; i++ )
            {
                Console.SetCursorPosition(Console.WindowLeft + Console.WindowWidth - 22, Console.WindowTop + i);
                Console.WriteLine("".PadLeft(22));
            }

            Console.BackgroundColor = BoardBackgroundColor;
            Console.ForegroundColor = BoardSpotsColor;

            for (int i = 0; i < BoardSpotsLayout.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowLeft + Console.WindowWidth - 21, Console.WindowTop + i + 3);
                Console.WriteLine(BoardSpotsLayout[i]);
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(Console.WindowLeft, Console.WindowTop + Console.WindowHeight - 1);
        }

        public void PlacePieces(Player[] playerArray)
        {
            Console.BackgroundColor = BoardBackgroundColor;
            int[] nestCoords = new int[2];
            int[] boardCoords = new int[2];
            int x;
            int y;

            foreach (Player player in playerArray)
            {
                Console.ForegroundColor = player.Color;
                switch (player.PlayerNumber)
                {
                    case 1:
                        nestCoords = new int[2] { Console.WindowLeft + Console.WindowWidth - 21, Console.WindowTop + 3 };
                        break;
                    case 2:
                        nestCoords = new int[2] { Console.WindowLeft + Console.WindowWidth - 5, Console.WindowTop + 3 };
                        break;
                    case 3:
                        nestCoords = new int[2] { Console.WindowLeft + Console.WindowWidth - 5, Console.WindowTop + 11 };
                        break;
                    case 4:
                        nestCoords = new int[2] { Console.WindowLeft + Console.WindowWidth - 21, Console.WindowTop + 11 };
                        break;
                }

                x = nestCoords[0];
                y = nestCoords[1];

                for (int i = 0; i < player.Pieces.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        x = nestCoords[0];
                    }

                    if (player.Pieces[i].InNest)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(i + 1);
                    }

                    if (i % 2 == 0)
                    {
                        x += 4;
                    }
                    else
                    {
                        y += 2;
                    }

                    if (!player.Pieces[i].InNest)
                    {
                        boardCoords = BoardPositionToCoords(player.Pieces[i].BoardPosition);
                        Console.SetCursorPosition(boardCoords[0], boardCoords[1]);
                        Console.Write(i + 1);
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(Console.WindowLeft, Console.WindowTop + Console.WindowHeight - 1);
        }

        public int[] BoardPositionToCoords (int boardPosition)
        {
            int[] position = new int[2] { Console.WindowLeft + Console.WindowWidth - 21, Console.WindowTop + 7 };

            if (boardPosition < 11)
            {
                if (boardPosition < 6)
                {
                    position[0] += (boardPosition - 1) * 2;
                    return position;
                }
                if (boardPosition < 10)
                {
                    position[0] += 8;
                    position[1] -= boardPosition - 5;
                }
                if (boardPosition == 10)
                {
                    position[0] += 8;
                    position[1] -= 4;
                }
                return position;
            }
            if (boardPosition < 21)
            {
                boardPosition -= 10;
                position[0] += 12;
                position[1] -= 4;
                if (boardPosition < 6)
                {
                    position[1] += (boardPosition - 1);
                    return position;
                }
                if (boardPosition < 10)
                {
                    position[0] += (boardPosition - 5) * 2;
                    position[1] += 4;
                }
                if (boardPosition == 10)
                {
                    position[0] += 8;
                    position[1] += 4;
                }
                return position;
            }
            if (boardPosition < 31)
            {
                boardPosition -= 20;
                position[0] += 20;
                position[1] += 2;
                if (boardPosition < 6)
                {
                    position[0] -= (boardPosition - 1) * 2;
                    return position;
                }
                if (boardPosition < 10)
                {
                    position[0] -= 8;
                    position[1] += boardPosition - 5;
                }
                if (boardPosition == 10)
                {
                    position[0] -= 8;
                    position[1] += 4;
                }
                return position;
            }
            if (boardPosition < 41)
            {
                boardPosition -= 30;
                position[0] += 8;
                position[1] += 6;
                if (boardPosition < 6)
                {
                    position[1] -= (boardPosition - 1);
                    return position;
                }
                if (boardPosition < 10)
                {
                    position[0] -= (boardPosition - 5) * 2;
                    position[1] -= 4;
                }
                if (boardPosition == 10)
                {
                    position[0] -= 8;
                    position[1] -= 4;
                }
                return position;
            }
            if (boardPosition < 200)
            {
                position[0] += (boardPosition - 100) * 2;
                position[1] += 1;
                return position;
            }
            if (boardPosition < 300)
            {
                position[0] += 10;
                //position[1] -= 4;
                position[1] += boardPosition - 200 - 4;
                return position;
            }
            if (boardPosition < 400)
            {
                //position[0] += 20;
                position[0] -= (boardPosition - 300) * 2 + 20;
                position[1] += 1;
                return position;
            }
            if (boardPosition < 500)
            {
                position[0] += 6;
                //position[1] += 6;
                position[1] -= boardPosition - 400 + 6;
                return position;
            }
            return position;
        }

        private string[] CreateBoardArray()
        {
            return new string[11] {
                ("o   o   o o o   o   o"),
                ("        o o o        "),
                ("o   o   o o o   o   o"),
                ("        o o o        "),
                ("o o o o o o o o o o o"),
                ("o o o o o   o o o o o"),
                ("o o o o o o o o o o o"),
                ("        o o o        "),
                ("o   o   o o o   o   o"),
                ("        o o o        "),
                ("o   o   o o o   o   o"),
            };
        }

        //for (int i = 0; i < numberOfPlayers; i++)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            nestLeftCorner[0] = BoardLeftCorner[0];
        //        }

        //        playerArray[i] = new Player(colorArray[i], nestLeftCorner, 0 + i*18);

        //        if (i % 2 == 0)
        //        {
        //            nestLeftCorner[0] += 16;
        //        }
        //        else
        //        {
        //            nestLeftCorner[1] += 8;
        //        }
        //    }
    }
}
