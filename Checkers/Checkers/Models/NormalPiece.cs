using Checkers.CheckersLogic;

namespace Checkers.Models;

public class NormalPiece : Piece
{
    public NormalPiece(Position position) : base(position)
    {
    }

    public override List<Position> GetMoves(Board board, Player player)
    {
        return this.GetPossibleMoves(board, player, this.Position);
    }
}