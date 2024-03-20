using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.CheckersLogic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Checkers.Models
{
    public partial class Piece : ObservableObject
    {
        public Position Position { get; }
        
        public bool HasSlain { get; set; }

        [ObservableProperty]
        public Color _color;

        public Piece(Position position)
        {
            this.Position = position;
            this.HasSlain = false;
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

        public List<Position> GetMoves(Board board, Player player)
        {
            // var direction = this.Color.Equals(Colors.Black) ? 1 : -1;
            // return new List<Position>
            // {
            //     new Position(this.Position.Row + direction, this.Position.Column + 1),
            //     new Position(this.Position.Row + direction, this.Position.Column - 1)
            // };
            return this.GetPossibleMoves(board, player, this.Position);
        }
        
        // TODO: Deze methode naar Board verplaatsen? Zorgen dat de Piece alleen zijn rechten teruggeeft (welke kant die op kan bewegen) 
        public List<Position> GetPossibleMoves(Board board, Player player, Position position, int horizontalDirection = 0)
        {
            var possibleMoves = new List<Position>();
            var verticalDirection = player.IsWhite ? -1 : 1;  // Adjust direction for player
            // Check diagonal moves in both directions
            for (var colOffset = -1; colOffset <= 1; colOffset += 2)
            { 
                if (colOffset < 0 && horizontalDirection > 0 || colOffset > 0 && horizontalDirection < 0)
                {
                    continue;
                }
                var newRow = position.Row + verticalDirection;
                var newCol = position.Column + colOffset;
                var targetPosition = new Position(newRow, newCol);
                
                // Check if move is within board bounds
                if (Board.IsInside(targetPosition))
                {
                    var opponentColor = player.IsWhite ? Colors.Black : Colors.White;
                    if (board.GetTileByPosition(targetPosition).Piece.Color == opponentColor &&
                        board.GetTileByPosition(position).Piece.Color == opponentColor)
                    {
                        continue;
                    }
                    // target position opponent piece and position is opponent piece
                    // Check if the target tile is empty
                    if (board.IsEmpty(targetPosition))
                    {
                        if (board.IsEmpty(position))
                        {
                            continue;
                        }
                        possibleMoves.Add(targetPosition);
                        // if (!HasSlain) continue;
                        if (board.GetTileByPosition(position).Piece.Color == player.Color)
                        {
                            continue;
                        }
                        var moves = GetPossibleMoves(board, player, targetPosition);
                        if (moves.Count > 0)
                        {
                            possibleMoves.AddRange(moves);
                        }
                    }
                    else if (board.GetTileByPosition(targetPosition).Piece.Color != player.Color)
                    {
                        // Capturing jump: Check for further jumps from the captured tile
                        this.HasSlain = true;
                        var jumpMoves = GetPossibleMoves(board, player, targetPosition,
                            targetPosition.Column - position.Column);
                        if (jumpMoves.Count > 0)
                        {
                            possibleMoves.AddRange(jumpMoves);
                        }
                    }
                }
            }
            this.HasSlain = false;

            return possibleMoves;
        }

        private bool CanJumpOver(Position targetPosition)
        {
            // Je kan niet in dezelfde kolom 2 rijen opschuiven. Je kan dus bijvoorbeeld niet van 4,3 naar 2,3 want dat is met slaan onmogelijk
            if (Math.Abs(targetPosition.Row - this.Position.Row) == 2 && targetPosition.Column == this.Position.Column)
            {
                return false;
            }

            return true;
            // var rowDiff = this.Position.Row - targetPosition.Row; // Calculate row and column difference
            // var colDiff = this.Position.Column - targetPosition.Column;
            //
            // // Check if the jump direction is valid for the player's color
            // if ((player.IsWhite && rowDiff != 1) || (!player.IsWhite && rowDiff != -1))
            // {
            //     return false;
            // }
            //
            // // Check if the tile behind the opponent's piece is empty (landing space)
            // var jumpRow = targetPosition.Row + rowDiff;
            // var jumpCol = targetPosition.Column + colDiff;
            // var jumpPosition = new Position(jumpRow, jumpCol);
            
            // return board.IsEmpty(targetPosition);
        }
        
        private bool CanMoveTo(Position position, Board board)
        {
            return Board.IsInside(position) && board.IsEmpty(position);
        }

        //public bool CanCaptureAt(Position position, Board board)
        //{
        //    if (!board.IsEmpty(position) && Board.IsInside(position))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        // Zoeken in de 2 rijen erna of ervoor
        // Recursief zoeken zodat je elke toegestane move krijgt?
        // Als er een piece op een tile zit, zoek net als normaal zodat je kan kijken of de volgende rij een tile available heeft.
    }
}
