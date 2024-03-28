using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic;

public class GameState
{
    public Board Board { get; }
    public Player PlayerTurn { get; }

    public GameState(Board board, Player playerTurn)
    {
        Board = board;
        PlayerTurn = playerTurn;
    }
}