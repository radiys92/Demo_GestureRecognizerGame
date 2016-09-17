using System;
using Helpers;

namespace Model.Api
{
    public enum GamePlayState
    {
        None,
        Init,
        StageStaring,
        ShowTemplateGesture,
        UserGestureInput,
        GesturesCompare,
        Pause,
        GameOver
    }

    public interface IGamePlayModel
    {
        HandledProperty<GamePlayState> State { get; } 
        HandledProperty<int> Score { get; }
        HandledProperty<int> Stage { get; }
        HandledProperty<TimeSpan> Time { get; }
    }
}