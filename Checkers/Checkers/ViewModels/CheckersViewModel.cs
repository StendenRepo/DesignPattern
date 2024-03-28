using Checkers.CheckersLogic;
using Checkers.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Checkers.ViewModels
{
    public partial class CheckersViewModel : ObservableObject
    {
        private Tile _selectedTile;

        [ObservableProperty] private Board _board;
        private Player PlayerTurn { get; set; }
        private Player Player1 { get; set; }
        private Player Player2 { get; set; }
        
        private GameStateHistory _gameStateHistory = new();
        private GameSettings Settings { get; }

        private int SetsDone { get; set; }

        public CheckersViewModel(GameSettings settings)
        {
            this.Board = new Board();
            this.Settings = settings;
            this.SetupGame();
            this.SetsDone = 0;
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
            _gameStateHistory.Add(CreateState());
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
                this._selectedTile = null;
                SwitchTurn(); // Move is now completed
                
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
                
                // Only create a state if a move has been completed
                if (this._selectedTile == null)
                {
                    _gameStateHistory.Add(CreateState());
                }
                
                this._selectedTile = tile;
                Board.ShowPossibleMoves(this._selectedTile, this.PlayerTurn);
                tile.Color = AppColors.SelectedTile;
            }
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
            if (gameState == null) return;
            Restore(gameState);
            Board.ResetHighlightedTiles();
        }

        private GameState CreateState()
        {
            var boardCopy = Board.Clone();
            return new GameState((Board)boardCopy, PlayerTurn); 
        }

        private void Restore(GameState state)
        {
            PlayerTurn = state.PlayerTurn;
            Board = state.Board;
        }
    }
}