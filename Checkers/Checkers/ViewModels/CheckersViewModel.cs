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

        private GameSettings Settings { get; }

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
            this.Player1 = new HumanPlayer(true);
            
            if (Settings.GameMode == GameMode.Single && Settings.Difficulty != null)
            {
                this.Player2 = new ComputerPlayer(false, Settings.Difficulty);
            }
            else
            {
                this.Player2 = new HumanPlayer(false);
            }
            
            this.PlayerTurn = this.Player1;
        }

        [ICommand]
        private async Task SelectTile(Tile tile)
        {
            // White tiles can't be selected
            if (tile.Color.Equals(AppColors.WhiteTile)) return;
            var playerColor = this.PlayerTurn.IsWhite ? AppColors.WhitePiece : AppColors.BlackPiece;

            if (this._selectedTile != null && tile.IsHighlighted())
            {
                Board.CapturePieces(tile.Position, this._selectedTile.Position);
                tile.ShowPiece(this._selectedTile.Piece.Color, this._selectedTile.Piece is KingDecorator);
                this._selectedTile.Piece.Hide();
                this.Board.ResetHighlightedTiles();
                await SwitchTurn();
                if (PlayerTurn is ComputerPlayer)
                {
                    await Task.Delay(1000);
                    PlayerTurn.MakeMove(this.Board);
                    await SwitchTurn();
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

        private async Task GameFinishedCheck()
        {
            var (whitePieces, blackPieces) = Board.GetPiecesCount();
            var winner = whitePieces <= 0 ? "Black" : "White";
            
            if (whitePieces <= 0 || blackPieces <= 0)
            {
                await Application.Current!.MainPage!.DisplayAlert("Game ended!", $"{winner} has won.", "Ok");
                await Application.Current!.MainPage!.Navigation.PopAsync();
            }
        }
        
        private async Task SwitchTurn()
        {
            await GameFinishedCheck();
            this.PlayerTurn = this.PlayerTurn == Player1 ? Player2 : Player1;
        }
    }
}