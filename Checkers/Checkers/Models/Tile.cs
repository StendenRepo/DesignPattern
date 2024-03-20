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
            this.Color = (this.Position.Row + this.Position.Column) % 2 != 0 ? Colors.Brown : Colors.White;
        }

        public void HidePiece()
        {
            this.Piece.Hide();
        }

        private void SetPieceColor()
        {
            if (this.Position.Row + this.Position.Column % 2 == 0 || this.Color.Equals(Colors.White)) return;
            this.Piece.SetStartingColor();
        }

        public bool HasPiece()
        {
            return !this.Piece.Color.Equals(Colors.Transparent);
        }

        public void Highlight()
        {
            if (this.HasPiece()) return;
            this.Color = Colors.Blue;
        }

        public bool IsHighlighted()
        {
            return this.Color.Equals(Colors.Blue);
        }

        public Color GetPieceColor()
        {
            return this.Piece.Color;
        }
    }
}
