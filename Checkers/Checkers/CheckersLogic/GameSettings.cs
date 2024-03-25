namespace Checkers.CheckersLogic;

public class GameSettings
{
    public GameMode GameMode { get; set; }
    public IStrategy Difficulty { get; set; }

    public GameSettings(GameMode gameMode, IStrategy difficulty)
    {
        this.GameMode = gameMode;
        this.Difficulty = difficulty;
    }

    public GameSettings(GameMode gameMode)
    {
        this.GameMode = gameMode;
        this.Difficulty = null;
    }
}

public enum GameMode
{
    Single,
    Double
}