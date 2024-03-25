using Checkers.CheckersLogic;

namespace Checkers.Models;

public class ComputerPlayer : Player
{
    public IStrategy Strategy { get; set; }
    public ComputerPlayer(bool isWhite, string name, IStrategy strategy) : base(isWhite, name)
    {
        this.Strategy = strategy;
    }

    public override void MakeMove(Board board)
    {
        var movesPerTile = new Dictionary<Position, List<Position>>();
        foreach (var tile in board.Tiles)
        {
            // Skip if a piece can't move
            if (!tile.Piece.Color.Equals(this.Color)) continue;
            
            var moves = board.GetPossibleMoves(tile, this);
            if (moves.Count > 0)
            {
                movesPerTile.Add(tile.Position, moves);
            }
        }
        
        var (startPosition, endPosition) = Strategy.CalculateBestMove(movesPerTile);
        var startTile = board.GetTileByPosition(startPosition);
        var endTile = board.GetTileByPosition(endPosition);
        
        board.GetPossibleMoves(startTile, this);
        board.CapturePieces(startPosition, endPosition);
        
        endTile.ShowPiece(startTile.Piece.Color);
        startTile.HidePiece();
    }
}