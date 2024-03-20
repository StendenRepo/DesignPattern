using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.CheckersLogic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Checkers.Models
{
    public abstract partial class Piece : ObservableObject
    {
        protected Position Position { get; }

        [ObservableProperty]
        public Color _color;

        protected Piece(Position position)
        {
            this.Position = position;
        }

        public void SetStartingColor()
        {
            if (this.Position.Row < 3)
            {
                this.Color = Colors.Black;
            }
            else if (this.Position.Row > 4)
            {
                this.Color = Colors.White;
            }
            else
            {
                this.Color = Colors.Transparent;
            }
        }

        public void Hide()
        {
            this.Color = Colors.Transparent;
        }

        public void Show(Color color)
        {
            this.Color = color;
        }

        public abstract List<Position> GetMoves(Board board, Player player);

        protected List<Position> GetPossibleMoves(Board board, Player player, Position position, int horizontalDirection = 0)
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
                if (!Board.IsInside(targetPosition)) continue;
                
                var targetTilePieceColor = board.GetTileByPosition(targetPosition).GetPieceColor();
                var positionTilePieceColor = board.GetTileByPosition(position).GetPieceColor();
                
                // Extra check to prevent searching the whole grid. If two neighbor pieces are from the same color, a move cannot be made.
                if (targetTilePieceColor.Equals(opponentColor) && positionTilePieceColor.Equals(opponentColor)) continue;
                
                // Check if the target tile is empty
                if (board.IsEmpty(targetPosition))
                {
                    // If the target position and current position are both empty. Skip the search for that part.
                    if (board.IsEmpty(position)) continue;

                    possibleMoves.Add(targetPosition);
                        
                    // Skip the search if the current position has a piece of the current player. Because u cannot jump own pieces
                    if (positionTilePieceColor.Equals(player.Color)) continue;
                        
                    // Search further
                    var moves = GetPossibleMoves(board, player, targetPosition);
                    possibleMoves.AddRange(moves);
                }
                else if (!targetTilePieceColor.Equals(player.Color)) // Check if the opponent piece can be jumped.
                {
                    var jumpMoves = GetPossibleMoves(board, player, targetPosition,
                        targetPosition.Column - position.Column);
                    possibleMoves.AddRange(jumpMoves);
                }
            }
            return possibleMoves;
        }
    }
}
