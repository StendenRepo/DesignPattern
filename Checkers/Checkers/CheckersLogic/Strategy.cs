using Checkers.Models;

namespace Checkers.CheckersLogic;

public interface IStrategy
{
    (Position startPosition, Position endPosition) CalculateBestMove(Dictionary<Position, List<Position>> possibleMoves);
}