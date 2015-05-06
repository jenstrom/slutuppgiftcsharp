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
            for (int i = 0; i < Console.WindowHeight; i++ )
            {
                Console.SetCursorPosition(Console.WindowLeft + Console.WindowWidth - 21, Console.WindowTop + i);
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
