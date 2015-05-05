using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift
{
    class Player
    {
        public string Name { get; set; }
        public Piece[] Pieces { get; set; }
        public ConsoleColor Color { get; set; }
        public int PlayerNumber { get; set; }



        public Player(ConsoleColor color, int playerNumber)
        {
            Pieces = new Piece[4] 
            {   
                new Piece(playerNumber),
                new Piece(playerNumber),
                new Piece(playerNumber),
                new Piece(playerNumber)
            };
            Color = color;
            PlayerNumber = playerNumber;
        }

        public void PlacePieces()
        {
            Console.ForegroundColor = Color;

            int[] pieceProgress = new int[4] { Pieces[0].Progress, Pieces[1].Progress, Pieces[2].Progress, Pieces[3].Progress };

            int[] inNest = Array.FindAll(pieceProgress, x => x == 0);

            if (inNest.Length > 0)
            {
                int[] placement = new int[2] { 0, 0 };


                for (int i = 0; i < inNest.Length; i++)
                {
                    if (i % 2 == 0)
                        placement[0] = 0;

                    Console.SetCursorPosition(placement[0], placement[1]);
                    Console.Write("X");

                    if (i % 2 == 0)
                        placement[0] += 4;
                    else
                        placement[1] += 2;
                }
            }

        }

        public bool[] GetMovablePieces(int dieRoll)
        {
            int furthestPiece = 0;
            bool[] pieceCanMove = new bool[4] {false, false, false, false};

            for (int i = 0; i < pieceCanMove.Length; i++)
            {
                if (Pieces[i].Progress > furthestPiece && Pieces[i].Progress < 45)
                {
                    furthestPiece = Pieces[i].Progress;
                }
            }
            Console.WriteLine(furthestPiece);

            for (int i = 0; i < pieceCanMove.Length; i++)
            {
                if (Pieces[i].InNest && (dieRoll != 1 && dieRoll != 6))
                {
                    continue;
                }

                if (Pieces[i].Progress != furthestPiece && Pieces[i].Progress + dieRoll >= furthestPiece)
                {
                    continue;
                }


                pieceCanMove[i] = true;
            }

            return pieceCanMove;


        }

        public void MovePiece(int piece, int dieRoll)
        {
            Pieces[piece].InNest = false;
            Pieces[piece].Progress += dieRoll;
        }

    }
}
