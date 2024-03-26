using System.Collections.ObjectModel;
using Checkers.Models;

namespace Checkers.CheckersLogic;

public class GameStateHistory
{
    private Stack<GameState> _states = new();

    public void Add(GameState state)
    {
        _states.Push(state);
    }
    
    public GameState Pop()
    {
        return _states.Count > 0 ? _states.Pop() : null;
    } 
}