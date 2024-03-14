using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic
{
    public class Board
    {
        public ObservableCollection<Tile> Tiles { get; set; }

        public Board()
        {
            this.Tiles = new ObservableCollection<Tile>();
        }

        public void InitializeBoard()
        {
            for (var col = 0; col < 8; col++)
            {
                for (var row = 0; row < 8; row++)
                {
                    var tile = new Tile(new Position(row, col));
                    Tiles.Add(tile);
                }
            }
        }

        public void Reset()
        {
            foreach (var tile in this.Tiles)
            {
                tile.SetStartingColor();
            }
        }

    }
}
