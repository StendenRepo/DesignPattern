using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic;

public class GameState
{
    public Board Board { get; private set; }
    public Player PlayerTurn { get; set; }

    public GameState(Board board, Player playerTurn)
    {
        Board = board;
        PlayerTurn = playerTurn;
    }
}