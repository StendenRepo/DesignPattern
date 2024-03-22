using Checkers.Models;

namespace Checkers.CheckersLogic;

public class BasicDifficulty : IStrategy
{
    public (Position startPosition, Position endPosition) CalculateBestMove(Dictionary<Position, List<Position>> possibleMoves)
    {
        // Picks a random move
        var (randomStartTile, value) = possibleMoves.Where(n => n.Value.Count != 0).MinBy(x => Guid.NewGuid());
        var randomEndTile = value.MinBy(x => Guid.NewGuid());
        return (randomStartTile, randomEndTile);
    }
}