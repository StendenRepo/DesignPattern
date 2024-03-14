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

        public IEnumerable<Position> GetMoves(Board board)
        {
            var direction = this.Color == Colors.Black ? 1 : -1;
            return new List<Position>
            {
                new Position(this.Position.Row + direction, this.Position.Column + 1),
                new Position(this.Position.Row + direction, this.Position.Column - 1)
            };
            
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
