using Checkers.CheckersLogic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Checkers.Models
{
    public partial class Tile : ObservableObject, ICloneable
    {
        public Position Position { get; set;  }

        [ObservableProperty] 
        private Piece _piece;

        [ObservableProperty] 
        private Color _color;

        public Tile(Position position)
        {
            this.Position = position;
            this.Piece = new NormalPiece(position);
            SetStandardColor();
            SetPieceColor();
        }

        private Tile()
        {
            
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

        public void ShowPiece(Color color, bool hasKing = false)
        {
            this.Piece.Color = color;
            if (this.Position.Row == 0 && GetPieceColor().Equals(AppColors.WhitePiece) || this.Position.Row ==7 && GetPieceColor().Equals(AppColors.BlackPiece) || hasKing)
            {
                this.Piece = new KingDecorator(this.Position, this.Piece);
                this.Piece.Color = color;
            }
            else
            {
                this.Piece = new NormalPiece(this.Position);
                this.Piece.Color = color;
            }
        }
        
        public object Clone()
        {
            // Creates a deep copy of the tile and returns it
            var clonedTile = new Tile()
            {
                Position = this.Position,
                Color = this.Color,
                Piece = new NormalPiece(Position),
            };
            clonedTile.Piece.Show(this.Piece.Color);
            return clonedTile;
        }
    }
}
