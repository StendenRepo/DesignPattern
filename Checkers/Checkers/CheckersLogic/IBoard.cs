using Checkers.Models;

namespace Checkers.CheckersLogic;

public interface IBoard
{
    List<Position> GetAvailableMoves(Position position, Player player, int horizontalDirection = 0);
}