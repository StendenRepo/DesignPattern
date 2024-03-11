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
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    bool isWhite = (row + col) % 2 != 0; // Alternate color for checkerboard pattern
                    ChessBoard.Add(new Tile { Row = row, Column = col, Color = isWhite ? Color.Parse("Brown") : Color.Parse("White")});
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
