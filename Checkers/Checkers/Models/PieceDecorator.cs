using Checkers.CheckersLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public abstract class PieceDecorator
    {
        Piece piece;
        protected PieceDecorator(Piece piece)
        {
            this.piece = piece;
        }
    }
}
