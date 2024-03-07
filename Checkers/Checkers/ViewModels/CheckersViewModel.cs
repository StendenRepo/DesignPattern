using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Checkers.Models;
using CommunityToolkit.Mvvm.Input;

namespace Checkers.ViewModels
{
    public class CheckersViewModel
    {
        //TODO: Create a class (Tile or something) that implements View so that it can be added to the view and made clickable / contain an image etc.
        public ICommand TileClickCommand { get; set; }
        public ObservableCollection<Tile> ChessBoard { get; set; }
        public CheckersViewModel()
        {
            TileClickCommand = new Command<Tile>(OnTileClicked);

            ChessBoard = new ObservableCollection<Tile>();
            InitializeBoard();
        }

        public void OnTileClicked(Tile tile)
        {
            Console.WriteLine("Hello");
        }


        public void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    bool isWhite = (row + col) % 2 != 0; // Alternate color for checkerboard pattern
                    ChessBoard.Add(new Tile { Row = row, Column = col, IsWhite = isWhite });
                }
            }
        }
    }
}
