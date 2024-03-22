using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic
{
    public class Board
    {
        public ObservableCollection<Tile> Tiles { get; } = new();
        public List<Position> CapturedPositions { get; } = new();

        public void InitializeBoard()
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

        public void ShowPossibleMoves(Tile tile, Player player)
        {
            this.CapturedPositions.Clear();
            this.ResetHighlightedTiles();
            var availablePositions = tile.Piece.GetMoves(this, player);
            foreach (var t in this.Tiles)
            {
                foreach (var pos in availablePositions.Where(pos => pos.Column == t.Position.Column && pos.Row == t.Position.Row))
                {
                    t.Highlight();
                }
            }
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

        public void CapturePieces()
        {
            foreach (var capturedPosition in CapturedPositions)
            {
                GetTileByPosition(capturedPosition).HidePiece();
            }
        }

        public Boolean CheckIfPieceIsAtEnd(Position position)
        {
            if(position.Row == 0 || position.Row == 7)
            {
                return true;
            }
            return false;
        }
    }
}
