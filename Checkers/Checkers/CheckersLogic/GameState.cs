using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic;

public class GameState
{
    public ObservableCollection<Tile> Tiles { get; private set; }

    public GameState(ObservableCollection<Tile> board)
    {
        Tiles = board;
    }
}