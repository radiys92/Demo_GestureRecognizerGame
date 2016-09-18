using System;
using Helpers;
using UnityEngine;

namespace Model.Api
{
    public enum GamePlayState
    {
        None,
        Init,
//        StageStarting,
        ShowTemplateGesture,
        UserGestureInput,
//        GesturesCompare,
        Pause,
        GameOver
    }

    public interface IGamePlayModel
    {
        HandledProperty<GamePlayState> State { get; } 
        HandledProperty<int> Score { get; }
//        HandledProperty<int> Stage { get; }
        HandledProperty<TimeSpan> Time { get; }
        HandledProperty<TimeSpan> InitCooldownTime { get; }
        HandledProperty<Vector2[]> Template { get; }
        HandledProperty<float> CurrentCooldown { get; }
        HandledProperty<int> Fails { get;}
    }
}