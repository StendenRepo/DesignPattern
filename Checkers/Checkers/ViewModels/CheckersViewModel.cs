using System.Collections.ObjectModel;
using Checkers.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Checkers.ViewModels
{
    public partial class CheckersViewModel : ObservableObject
    {
        public ObservableCollection<Tile> ChessBoard { get; set; }
        public CheckersViewModel()
        {
            ChessBoard = new ObservableCollection<Tile>();
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    var tile = new Tile(row, col);
                    ChessBoard.Add(tile);
                }
            }
        }

        [ICommand]
        public void SelectTile(Tile selectedTile)
        {
            selectedTile.Color = Color.Parse("Green");
        }
    }
}
