using System.Collections.Generic;
using GCon;
using Helpers;
using UnityEngine;

namespace Model
{
    public interface IGameFlowModel
    {
        HandledProperty<GameFlowModel.GameStates> GameState { get; }
        HandledProperty<Gesture> FirstGesture { get; }
        HandledProperty<Gesture> SecondGesture { get; }
        HandledProperty<float> ComparsionScore { get; }
        List<LineRenderer> LineRenderers { get;}
    }
}