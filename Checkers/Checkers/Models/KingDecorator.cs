using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class KingDecorator : PieceDecorator
    {
        public KingDecorator(Piece piece) : base(piece)
        {
        }
    }
}
