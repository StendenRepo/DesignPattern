using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic
{
    public class Board
    {
        public ObservableCollection<Tile> Tiles { get; } = new();
        public List<Position> CapturedPositions { get; } = new();

        public void Initialize()
        {
            for (var col = 0; col < 8; col++)
            {
                for (var row = 0; row < 8; row++)
                {
                    var tile = new Tile(new Position(row, col));
                    Tiles.Add(tile);
                }
            }
        }

        public void Reset()
        {
            foreach (var tile in this.Tiles)
            {
                tile.SetStandardColor();
            }
        }

        public bool IsEmpty(Position position)
        {
            return !this.GetTileByPosition(position).HasPiece();
        }

        public static bool IsInside(Position position)
        {
            return position.Row is >= 0 and <= 7 && position.Column is >= 0 and <= 7;
        }

        public List<Position> ShowPossibleMoves(Tile tile, Player player)
        {
            this.ResetHighlightedTiles();
            var availablePositions = GetPossibleMoves(tile, player);
            foreach (var t in this.Tiles)
            {
                foreach (var pos in availablePositions.Where(pos => pos.Column == t.Position.Column && pos.Row == t.Position.Row))
                {
                    t.Highlight();
                }
            }

            return availablePositions;
        }

        public List<Position> GetPossibleMoves(Tile tile, Player player)
        {
            this.CapturedPositions.Clear();
            var availablePositions = tile.Piece.GetMoves(this, player);
            return availablePositions;
        }

        public void ResetHighlightedTiles()
        {
            foreach (var t in this.Tiles)
            {
                t.SetStandardColor();
            }
        }

        public Tile GetTileByPosition(Position pos)
        {
            return Tiles.First(t => t.Position.Column == pos.Column && t.Position.Row == pos.Row);
        }

        public void CapturePieces(Position startPosition, Position endPosition)
        {
            if (Math.Abs(startPosition.Column - endPosition.Column) == 1 ||
                Math.Abs(startPosition.Row - endPosition.Row) == 1) return;
            
            foreach (var capturedPosition in CapturedPositions)
            {
                GetTileByPosition(capturedPosition).HidePiece();
            }
        }
    }
}
