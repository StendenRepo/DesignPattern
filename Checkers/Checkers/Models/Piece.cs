using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Checkers.Models
{
    public partial class Piece : ObservableObject
    {
        private readonly int _row;
        private readonly int _column;

        [ObservableProperty]
        public Color _color;

        public Piece(int row, int column)
        {
            this._row = row;
            this._column = column;
        }

        public void SetStartingColor()
        {
            if (this._column < 3)
            {
                this.Color = Color.Parse("Black");
            }
            else if (this._column > 4)
            {
                this.Color = Color.Parse("White");
            }
            else
            {
                this.Color = this.Color;
            }

        }
    }
}
