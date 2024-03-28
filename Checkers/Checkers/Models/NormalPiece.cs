using Checkers.CheckersLogic;

namespace Checkers.Models;

public class NormalPiece : Piece
{
    public NormalPiece(Position position) : base(position)
    {
    }

    public override List<Position> GetMoves(Board board, Player player)
    {
        var verticalDirection = player.IsWhite ? -1 : 1;
        return this.GetPossibleMoves(board, player, this.Position, verticalDirection);
        
    }
}