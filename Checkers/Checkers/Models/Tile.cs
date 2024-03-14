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
            this.Piece = new Piece(position);
            SetStartingColor();
        }

        public void SetStartingColor()
        {
            this.Color = (this.Position.Row + this.Position.Column) % 2 != 0 ? Colors.Brown : Colors.White;
            SetPieceColor();
        }

        public void HidePiece()
        {
            this.Piece.Hide();
        }

        private void SetPieceColor()
        {
            if (this.Position.Row + this.Position.Column % 2 == 0 || this.Color == Colors.White) return;
            this.Piece.SetStartingColor();
        }
    }
}
