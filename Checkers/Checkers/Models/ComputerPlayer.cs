using Checkers.CheckersLogic;

namespace Checkers.Models;

public class ComputerPlayer : Player
{
    public IStrategy Strategy { get; set; }
    public ComputerPlayer(bool isWhite, string name) : base(isWhite, name)
    {
    }

    public void SelectMove()
    {
        // Strategy.Execute();
    }
}