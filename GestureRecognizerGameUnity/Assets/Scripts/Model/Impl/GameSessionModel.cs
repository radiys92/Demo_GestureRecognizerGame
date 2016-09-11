using GCon;

public class GameSessionModel : IGameSessionModel
{
    public enum GameStates
    {
        None,
        DrawLine1,
        DrawLine2,
        Compare
    }

    public GameSessionModel()
    {
        GameState = new HandledProperty<GameStates>(GameStates.None);
        FirstGesture = new HandledProperty<Gesture>();
        SecondGesture = new HandledProperty<Gesture>();
        ComparsionScore = new HandledProperty<float>();
    }

    public HandledProperty<GameStates> GameState { get; private set; }
    public HandledProperty<Gesture> FirstGesture { get; private set; }
    public HandledProperty<Gesture> SecondGesture { get; private set; }
    public HandledProperty<float> ComparsionScore { get; private set; }
}