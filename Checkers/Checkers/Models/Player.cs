using Checkers.CheckersLogic;

namespace Checkers.Models
{
    public abstract class Player
    {
        public bool IsWhite { get; }
        public Color Color { get; }

        protected Player(bool isWhite)
        {
            this.IsWhite = isWhite;
            this.Color = isWhite ? AppColors.WhitePiece : AppColors.BlackPiece;
        }

        public virtual void MakeMove(Board board)
        {
            
        }
    }
}
