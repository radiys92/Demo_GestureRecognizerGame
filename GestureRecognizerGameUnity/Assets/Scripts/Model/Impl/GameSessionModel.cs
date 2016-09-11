public class GameSessionModel : IGameSessionModel
{
    public GameSessionModel()
    {
    }

    public enum GameStates
    {
        None,
        DrawLine1,
        DrawLine2,
        Compare
    }

    private readonly HandledProperty<GameStates> _gameState = new HandledProperty<GameStates>(GameStates.None);

    public HandledProperty<GameStates> GameState
    {
        get { return _gameState; }
    }
}