using Checkers.Models;

namespace Checkers.CheckersLogic;

public class BasicDifficulty : IStrategy
{
    public (Position startPosition, Position endPosition) CalculateBestMove(Dictionary<Position, List<Position>> possibleMoves)
    {
        // Picks a random move
        var (randomStartPosition, value) = possibleMoves.Where(n => n.Value.Count != 0).MinBy(x => Guid.NewGuid());
        var randomEndPosition = value.MinBy(x => Guid.NewGuid());
        return (randomStartPosition, randomEndPosition);
    }
}