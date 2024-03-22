using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.CheckersLogic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Checkers.Models
{
    public partial class Tile : ObservableObject
    {
        public Position Position { get; }
        public bool IsSelected { get; set; }

        public Piece Piece { get; set; }

        [ObservableProperty] 
        public Color _color;

        public Tile(Position position)
        {
            this.IsSelected = false;
            this.Position = position;
            this.Piece = new NormalPiece(position);
            SetStandardColor();
            SetPieceColor();
        }

        public void SetStandardColor()
        {
            this.Color = (this.Position.Row + this.Position.Column) % 2 != 0 ? AppColors.BlackTile : AppColors.WhiteTile;
        }

        public void HidePiece()
        {
            this.Piece.Hide();
        }

        private void SetPieceColor()
        {
            this.Piece.SetStartingColor();
            if (this.Position.Row + this.Position.Column % 2 == 0 || this.Color.Equals(AppColors.WhiteTile))
            {
                this.Piece.Color = Colors.Transparent;
            }
        }

        public bool HasPiece()
        {
            return !this.Piece.Color.Equals(Colors.Transparent);
        }

        public void Highlight()
        {
            if (this.HasPiece()) return;
            this.Color = AppColors.HighlightedTile;
        }

        public bool IsHighlighted()
        {
            return this.Color.Equals(AppColors.HighlightedTile);
        }

        public Color GetPieceColor()
        {
            return this.Piece.Color;
        }

        public void ShowPiece(Color color)
        {
            this.Piece.Color = color;
        }
    }
}
