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
            var fromTile = board.Tiles.First(t => t.Position.Column == this.FromPosition.Column && t.Position.Row == this.FromPosition.Row);
            var toTile = board.Tiles.First(t => t.Position.Column == this.ToPosition.Column && t.Position.Row == this.ToPosition.Row);
            fromTile.HidePiece();
            toTile.HidePiece();
        }
    }
}
