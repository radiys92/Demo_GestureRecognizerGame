using System.Collections.Generic;
using GCon;
using Helpers;
using Model;
using UnityEngine;

public class GameFlowModel : IGameFlowModel
{
    public enum GameStates
    {
        None,
        DrawLine1,
        DrawLine2,
        Compare
    }

    public GameFlowModel()
    {
        GameState = new HandledProperty<GameStates>(GameStates.None);
        FirstGesture = new HandledProperty<Gesture>();
        SecondGesture = new HandledProperty<Gesture>();
        ComparsionScore = new HandledProperty<float>();
        LineRenderers = new List<LineRenderer>();
    }

    public HandledProperty<GameStates> GameState { get; private set; }
    public HandledProperty<Gesture> FirstGesture { get; private set; }
    public HandledProperty<Gesture> SecondGesture { get; private set; }
    public HandledProperty<float> ComparsionScore { get; private set; }
    public List<LineRenderer> LineRenderers { get; private set; }
}