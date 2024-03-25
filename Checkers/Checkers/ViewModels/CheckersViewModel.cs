using Checkers.CheckersLogic;
using Checkers.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Checkers.ViewModels
{
    public partial class CheckersViewModel : ObservableObject
    {
        private Tile _selectedTile;
        public Board Board { get; }
        private Player PlayerTurn { get; set; }
        private Player Player1 { get; set; }
        private Player Player2 { get; set; }

        public GameSettings Settings { get; private set; }

        public CheckersViewModel(GameSettings settings)
        {
            this.Board = new Board();
            this.Settings = settings;
            this.SetupGame();
        }

        private void SetupGame()
        {
            this.Board.Initialize();
            this._selectedTile = null;
            this.Player1 = new HumanPlayer(true, "player1");
            
            if (Settings.GameMode == GameMode.Single && Settings.Difficulty != null)
            {
                this.Player2 = new ComputerPlayer(false, "player2", Settings.Difficulty);
            }
            else
            {
                this.Player2 = new HumanPlayer(false, "player2");
            }
            
            this.PlayerTurn = this.Player1;
        }

        [ICommand]
        public async Task SelectTile(Tile tile)
        {
            // White tiles can't be selected
            if (tile.Color.Equals(AppColors.WhiteTile)) return;
            var playerColor = this.PlayerTurn.IsWhite ? AppColors.WhitePiece : AppColors.BlackPiece;

            if (this._selectedTile != null && tile.IsHighlighted())
            {
                Board.CapturePieces(tile.Position, this._selectedTile.Position);
                tile.Piece.Show(this._selectedTile.Piece.Color);
                this._selectedTile.Piece.Hide();
                this.Board.ResetHighlightedTiles();
                SwitchTurn();
                if (PlayerTurn is ComputerPlayer)
                {
                    await Task.Delay(1000);
                    PlayerTurn.MakeMove(this.Board);
                    SwitchTurn();
                }
            }
            else
            {
                if (!this.PlayerTurn.Color.Equals(tile.Piece.Color)) return;
                if (!tile.HasPiece()) return;
                this._selectedTile = tile;
                Board.ShowPossibleMoves(this._selectedTile, this.PlayerTurn);
                tile.Color = AppColors.SelectedTile;
            }
        }

        [ICommand]
        public void ResetGame()
        {
            this.Board.Reset();
        }
        
        private void SwitchTurn()
        {
            this.PlayerTurn = this.PlayerTurn == Player1 ? Player2 : Player1;
        }
    }
}