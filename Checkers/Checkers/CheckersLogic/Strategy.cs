namespace Checkers.CheckersLogic;

public interface IStrategy
{
    void Execute(Position currentPosition, List<Position> possibleMoves);
}