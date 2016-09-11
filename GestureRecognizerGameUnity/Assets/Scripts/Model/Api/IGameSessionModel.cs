using System.Collections.Generic;
using GCon;
using UnityEngine;

public interface IGameSessionModel
{
    HandledProperty<GameSessionModel.GameStates> GameState { get; }
    HandledProperty<Gesture> FirstGesture { get; }
    HandledProperty<Gesture> SecondGesture { get; }
    HandledProperty<float> ComparsionScore { get; }
    List<LineRenderer> LineRenderers { get;}
}