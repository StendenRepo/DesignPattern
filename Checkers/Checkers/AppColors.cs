namespace Checkers;

public static class AppColors
{
    public static Color WhiteTile { get; set; } = Color.FromRgb(240, 217, 178);
    public static Color BlackTile { get; set; } = Color.FromRgb(181, 136, 96);
    public static Color WhitePiece { get; set; } = Colors.White;
    public static Color BlackPiece { get; set; } = Colors.Black;
    public static Color SelectedTile { get; set; } = Color.FromRgb(169, 169, 169);
    public static Color HighlightedTile { get; set; } = Color.FromRgb( 105, 105, 105);
}