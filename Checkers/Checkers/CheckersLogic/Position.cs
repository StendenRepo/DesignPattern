using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.CheckersLogic
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int col)
        {
            this.Row = row;
            this.Column = col;
        }

    }
}
