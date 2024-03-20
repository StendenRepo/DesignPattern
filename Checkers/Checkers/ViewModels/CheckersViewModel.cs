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
        private Player PlayerTurn { get; set; }
        private Player Player1 { get; }
        private Player Player2 { get; }

        public CheckersViewModel()
        {
            this.Board = new Board();
            this.Board.InitializeBoard();
            this._selectedTile = null;
            this.Player1 = new HumanPlayer(true, "player1");
            this.Player2 = new HumanPlayer(false, "player2");
            this.PlayerTurn = this.Player1;
        }

        [ICommand]
        public void SelectTile(Tile tile)
        {
            if (tile.Color.Equals(Colors.White)) return;
            var playerColor = this.PlayerTurn.IsWhite ? Colors.White : Colors.Black;
            
            if (this._selectedTile != null && tile.IsHighlighted())
            {
                tile.Piece.Show(this._selectedTile.Piece.Color);
                this._selectedTile.Piece.Hide();
                this.Board.ResetHighlightedTiles();
                this.PlayerTurn = this.PlayerTurn == Player1 ? Player2 : Player1;
            }
            else
            {
                if (!this.PlayerTurn.Color.Equals(tile.Piece.Color)) return ;
                if (!tile.HasPiece()) return;
                this._selectedTile = tile;
                Board.ShowPossibleMoves(this._selectedTile, this.PlayerTurn);
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
