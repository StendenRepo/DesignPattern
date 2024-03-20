using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic
{
    public class Board : IBoard
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
            return !this.GetTileByPosition(position).HasPiece();
        }

        public static bool IsInside(Position position)
        {
            return position.Row is >= 0 and <= 7 && position.Column is >= 0 and <= 7;
        }

        public void ShowPossibleMoves(Tile tile, Player player)
        {
            this.ResetHighlightedTiles();
            var availablePositions = tile.Piece.GetMoves(this, player);
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

        public List<Position> GetAvailableMoves(Position position, Player player, int horizontalDirection = 0)
        {
            var possibleMoves = new List<Position>();
            var verticalDirection = player.IsWhite ? -1 : 1;  // Adjust direction for player
            var opponentColor = player.IsWhite ? Colors.Black : Colors.White;
            
            // Check diagonal moves in both directions
            for (var colOffset = -1; colOffset <= 1; colOffset += 2)
            { 
                if (colOffset < 0 && horizontalDirection > 0 || colOffset > 0 && horizontalDirection < 0) continue;
                
                var newRow = position.Row + verticalDirection;
                var newCol = position.Column + colOffset;
                var targetPosition = new Position(newRow, newCol);
                
                // Check if move is within board bounds
                if (!IsInside(targetPosition)) continue;
                
                var targetTilePieceColor = GetTileByPosition(targetPosition).GetPieceColor();
                var positionTilePieceColor = GetTileByPosition(position).GetPieceColor();
                
                // Extra check to prevent searching the whole grid. If two neighbor pieces are from the same color, a move cannot be made.
                if (targetTilePieceColor.Equals(opponentColor) && positionTilePieceColor.Equals(opponentColor)) continue;
                
                // Check if the target tile is empty
                if (IsEmpty(targetPosition))
                {
                    // If the target position and current position are both empty. Skip the search for that part.
                    if (IsEmpty(position)) continue;

                    possibleMoves.Add(targetPosition);
                        
                    // Skip the search if the current position has a piece of the current player. Because u cannot jump own pieces
                    if (positionTilePieceColor.Equals(player.Color)) continue;
                        
                    // Search further
                    var moves = GetAvailableMoves(targetPosition, player);
                    possibleMoves.AddRange(moves);
                }
                else if (!targetTilePieceColor.Equals(player.Color)) // Check if the opponent piece can be jumped.
                {
                    var jumpMoves = GetAvailableMoves( targetPosition, player,
                        targetPosition.Column - position.Column);
                    possibleMoves.AddRange(jumpMoves);
                }
            }
            return possibleMoves;
        }
    }
}
