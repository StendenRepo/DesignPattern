using Checkers.CheckersLogic;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Models
{
    public class KingDecorator : PieceDecorator
    {
        public KingDecorator(Position position, Piece piece) : base(position, piece)
        {
            this.BorderColor = Colors.Yellow;
        }

        public override List<Position> GetMoves(Board board, Player player)
        {
            var possibleMoves = GetPossibleMoves(board, player, this.Position, 1);
            possibleMoves.AddRange(GetPossibleMoves(board, player, this.Position, -1));
            return possibleMoves;
        }
    }
}
