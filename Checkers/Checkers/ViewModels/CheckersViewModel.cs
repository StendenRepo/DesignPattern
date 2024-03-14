using System.Collections.ObjectModel;
using Checkers.CheckersLogic;
using Checkers.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Checkers.ViewModels
{
    public partial class CheckersViewModel : ObservableObject
    {
        private Tile _selectedTile;
        public Board Board { get; set; }
        public Player PlayerTurn { get; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public CheckersViewModel()
        {
            this.Board = new Board();
            this.Board.InitializeBoard();
            this._selectedTile = null;
            this.Player1 = new Player(true, "player1");
            this.PlayerTurn = new Player(false, "player2");
        }

        [ICommand]
        public void SelectTile(Tile tile)
        {
            if (tile.Color == Colors.White) return;

            if (this._selectedTile != null && tile.IsHighlighted())
            {
                tile.Piece.Show(this._selectedTile.Piece.Color);
                this._selectedTile.Piece.Hide();
                this.Board.ResetHighlightedTiles();
            }
            else
            {
                if (!tile.HasPiece()) return;
                this._selectedTile = tile;
                Board.ShowPossibleMoves(this._selectedTile);
                tile.Color = Colors.Green;
            }

            // Bij een turn switch moet de selectedtile weer op null
        }

        [ICommand]
        public void ResetGame()
        {
            this.Board.Reset();
        }

        
    }
}
