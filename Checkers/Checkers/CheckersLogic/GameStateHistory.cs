using System.Collections.ObjectModel;
using System.Diagnostics;
using Checkers.Models;

namespace Checkers.CheckersLogic;

public class GameStateHistory
{
    private readonly Stack<GameState> _states = new();

    public void Add(GameState state)
    {
        _states.Push(state);
        Debug.WriteLine(_states.Count);
    }
    
    public GameState Undo()
    {
        Debug.WriteLine(_states.Count);
        return _states.Count > 0 ? _states.Pop() : null;
    } 
}