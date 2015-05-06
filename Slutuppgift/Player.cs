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

        public Board Board { get; set; }

        public Player(ConsoleColor color, int playerNumber, Board board)
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
            Board = board;
        }

        
        public bool[] GetMovablePieces(int dieRoll)
        {
            bool[] movablePieces = new bool[4] {false, false, false, false};
            bool pieceCanMove = false;

            for (int i = 0; i < movablePieces.Length; i++)
            {
                pieceCanMove = false;

                if (Pieces[i].InNest && (dieRoll != 1 && dieRoll != 6))
                {
                    continue;
                }

                if (Pieces[i].Progress == 45)
                {
                    continue;
                }

                for (int j = 0; j < movablePieces.Length; j++ )
                {
                    pieceCanMove = true;

                    if (Pieces[i].InNest)
                    {
                        if (Pieces[i].Progress + dieRoll >= Pieces[j].Progress && !Pieces[j].InNest)
                        {
                            pieceCanMove = false;
                            break;
                        }
                    }
                    else
                    {
                        if (Pieces[i].Progress < Pieces[j].Progress 
                            && (Pieces[i].Progress + dieRoll) >= Pieces[j].Progress
                            && Pieces[i].Progress + dieRoll < 45)
                        {
                            pieceCanMove = false;
                            break;
                        }
                    }
                }

                movablePieces[i] = pieceCanMove;
            }

            return movablePieces;
        }

    }
}
