namespace Checkers.Models
{
    public abstract class Player
    {
        public bool IsWhite { get; }
        public string Name { get; }
        public Color Color { get; }

        protected Player(bool isWhite, string name)
        {
            this.IsWhite = isWhite;
            this.Name = name;
            this.Color = isWhite ? AppColors.WhitePiece : AppColors.BlackPiece;
        }
    }
}
