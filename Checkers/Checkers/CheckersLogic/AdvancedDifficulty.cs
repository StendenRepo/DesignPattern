namespace Checkers.CheckersLogic;

public class AdvancedDifficulty : IStrategy
{
    public (Position startPosition, Position endPosition) CalculateBestMove(Dictionary<Position, List<Position>> possibleMoves)
    {
        var bestMoves = possibleMoves
            .ToDictionary(move => move.Key, move => CalculateBestMoveForPosition(move.Key, move.Value));
        foreach (var move in bestMoves.Where(move => move.Value.Row == 7))
        {
            // Upgrading to king is always favoured
            return (move.Key, move.Value);
        }
        
        // Do the move that is most positions forward
        var bestMove = bestMoves.MaxBy(p => Math.Abs(p.Key.Row - p.Value.Row));
        
        return (bestMove.Key, bestMove.Value);
    }

    private Position CalculateBestMoveForPosition(Position position, List<Position> positions)
    {
        // Check if there are available king moves
        var movesToKing = positions.Where(p => p.Row == 7).ToList();
        
        // Check if there are available jump moves
        var jumpMoves = positions.Where(p => Math.Abs(p.Row - position.Row) > 1).ToList();
        
        // A move that will upgrade to king will always be favoured
        if (movesToKing.Any())
        {
            return movesToKing.First();
        }
        
        // If there are jump moves, return the one with most jumps. If there are no jump moves return a random move
        return jumpMoves.Any() ? jumpMoves.OrderByDescending(p => p.Row).First() : positions.MinBy(p => Guid.NewGuid());
    }
}