using Checkers.CheckersLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public abstract class PieceDecorator : Piece
    {
        protected Piece piece;

        protected PieceDecorator(Position position, Piece piece) : base(position)
        {
            this.piece = piece;
        }

        public override List<Position> GetMoves(Board board, Player player)
        {
            return piece.GetMoves(board, player);
        }
    
    }
}
