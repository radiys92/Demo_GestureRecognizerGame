using System.Collections.Generic;
using GCon;
using Helpers;
using Model.Api;
using UnityEngine;

namespace Model.Impl
{
    public class GameFlowModel : IGameFlowModel
    {
        public HandledProperty<GameStates> GameState { get; } = new HandledProperty<GameStates>(GameStates.None);
        public HandledProperty<Gesture> FirstGesture { get; } = new HandledProperty<Gesture>();
        public HandledProperty<Gesture> SecondGesture { get; } = new HandledProperty<Gesture>();
        public HandledProperty<float> ComparsionScore { get; } = new HandledProperty<float>();
        public List<LineRenderer> LineRenderers { get; } = new List<LineRenderer>();
    }

    public enum GameStates
    {
        None,
        MainMenu,
        GamePlay,
        Pause,
        GameOver
    }
}