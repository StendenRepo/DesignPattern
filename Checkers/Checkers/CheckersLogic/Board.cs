using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic
{
    public class Board
    {
        public ObservableCollection<Tile> Tiles { get; }

        public Board()
        {
            this.Tiles = new ObservableCollection<Tile>();
        }

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
            return this.GetTileByPosition(position).HasPiece();
        }

        public static bool IsInside(Position position)
        {
            return position.Row is >= 0 and <= 8 && position.Column is >= 0 and <= 8;
        }

        public void ShowPossibleMoves(Tile tile)
        {
            this.ResetHighlightedTiles();
            var availablePositions = tile.Piece.GetMoves(this);
            foreach (var t in this.Tiles)
            {
                foreach (var pos in availablePositions)
                {
                    if (pos.Column == t.Position.Column && pos.Row == t.Position.Row)
                    {
                        t.Highlight();
                    }
                }
            }
        }

        public void ResetHighlightedTiles()
        {
            // kan nog veranderd worden naar een aparte lijst met tiles zodat je niet elke keer door de hele lijst heen hoeft.
            foreach (var t in this.Tiles)
            {
                t.SetStandardColor();
            }
        }

        public Tile GetTileByPosition(Position pos)
        {
            return Tiles.First(t => t.Position.Column == pos.Column && t.Position.Row == pos.Row);
        }

    }
}
