using System.Collections.Generic;
using GCon;
using Helpers;
using Model.Impl;
using UnityEngine;

namespace Model.Api
{
    public interface IGameFlowModel
    {
        HandledProperty<GameStates> GameState { get; }
        HandledProperty<Gesture> FirstGesture { get; }
        HandledProperty<Gesture> SecondGesture { get; }
        HandledProperty<float> ComparsionScore { get; }
        List<LineRenderer> LineRenderers { get;}
    }
}