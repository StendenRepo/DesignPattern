using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Models;

namespace Checkers.CheckersLogic
{
    public class Move
    {
        public Position FromPosition { get; }
        public Position ToPosition { get; }

        public Move(Position from, Position to)
        {
            this.FromPosition = from;
            this.ToPosition = to;
        }
        public void Execute(Board board)
        {
            var fromTile = board.GetTileByPosition(FromPosition);
            var toTile = board.GetTileByPosition(ToPosition);
            fromTile.HidePiece();
            toTile.HidePiece();
        }
    }
}
