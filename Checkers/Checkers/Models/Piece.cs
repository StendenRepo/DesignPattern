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

        [ObservableProperty]
        public Color _color;

        public Piece(Position position)
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
                this.Color = this.Color;
            }

        }

        public void Hide()
        {
            this.Color = Colors.Brown;

        }

        //public IEnumerable<Move> GetMoves()
        //{
        //    if (this.Color == Colors.Black)
        //    {

        //    }
        //    List<Move> moves = new List<Move>
        //    {
        //        new Move(this.Position.Column - 1,              )
        //    };
        //}
    }
}
