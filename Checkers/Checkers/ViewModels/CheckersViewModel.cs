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

        public CheckersViewModel()
        {
            this.Board = new Board();
            this.Board.InitializeBoard();
            this._selectedTile = null;
        }

        [ICommand]
        public void SelectTile(Tile tile)
        {
            if (tile.Color == Colors.White) return;

            this._selectedTile = tile;
            tile.Color = Colors.Green;
        }

        [ICommand]
        public void ResetGame()
        {
            this.Board.Reset();
        }
    }
}
