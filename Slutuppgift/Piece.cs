using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift
{
    class Piece
    {
        private int _progress;
        public string Name { get; set; }
        public int Owner { get; set; }
        public int StartPosition { get; set; }
        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                BoardPosition = _progress + StartPosition;

                if (BoardPosition > 40)
                {
                    BoardPosition -= 40;
                }

                if (_progress > 40)
                {
                    BoardPosition = 100 * Owner + _progress - 40;
                }
            }
        }
        public int BoardPosition { get; private set; }
        public bool InNest { get; set; }

        public Piece(int owner)
        {
            Owner = owner;
            Progress = 0;
            InNest = true;
            StartPosition = (Owner - 1) * 10;
        }

    }
}
