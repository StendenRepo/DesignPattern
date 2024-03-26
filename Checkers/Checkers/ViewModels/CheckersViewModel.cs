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
        
        private GameStateHistory _gameStateHistory = new();
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
        private async Task SelectTile(Tile tile)
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
                
                //TODO Add player state
            }
            else
            {
                if (!this.PlayerTurn.Color.Equals(tile.Piece.Color)) return;
                if (!tile.HasPiece()) return;
                this._selectedTile = tile;
                Board.ShowPossibleMoves(this._selectedTile, this.PlayerTurn);
                tile.Color = AppColors.SelectedTile;
            }
            _gameStateHistory.Add(Board.CreateState());
        }

        [ICommand]
        private void ResetGame()
        {
            this.Board.Reset();
        }

        private void SwitchTurn()
        {
            this.PlayerTurn = this.PlayerTurn == Player1 ? Player2 : Player1;
        }

        [ICommand]
        private void Undo()
        {
            var gameState = _gameStateHistory.Pop();
            if (gameState != null)
            {
                Board.Restore(gameState);
            }
        }
    }
}