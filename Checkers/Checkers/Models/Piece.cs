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

        public List<Position> GetMoves(IBoard board, Player player)
        {
            return board.GetAvailableMoves(this.Position, player);
        }
    }
}
