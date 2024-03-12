using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Checkers.Models
{
    public partial class Tile : ObservableObject
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsSelected { get; set; }

        public Piece Piece { get; set; }

        [ObservableProperty] 
        public Color _color;

        public Tile(int row, int col)
        {
            this.IsSelected = false;
            this.Row = row;
            this.Column = col;
            this.Piece = new Piece(row, col);
            SetStartingColor();
        }

        public void SetStartingColor()
        {
            this.Color = (this.Row + this.Column) % 2 != 0 ? Color.Parse("Brown") : Color.Parse("White");
            SetPieceColor();
        }

        private void SetPieceColor()
        {
            if (this.Row + this.Column % 2 == 0 || this.Color == Color.Parse("White")) return;
            this.Piece.SetStartingColor();
        }
    }
}
