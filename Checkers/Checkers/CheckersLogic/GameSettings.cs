namespace Checkers.CheckersLogic;

public class GameSettings
{
    public GameMode GameMode { get; set; }
    public Difficulty Difficulty { get; set; }

    public GameSettings(GameMode gameMode, Difficulty difficulty)
    {
        this.GameMode = gameMode;
        this.Difficulty = difficulty;
    }
}

public enum GameMode
{
    Single,
    Double
}

public enum Difficulty
{
    Basic,
    Advanced,
    None
}