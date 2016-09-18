using System;
using Logic.Signals;
using Model.Api;
using Model.Impl;
using UIView.Windows;
using UnityEngine;

namespace UIView.Mediator
{
    public class GamePlayHudMediator : WindowViewMediator<GamePlayHud>
    {
        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        [Inject]
        public IGamePlayModel Session { get; private set; }

        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; }

        public override void OnRemove()
        {
            GameFlow.GameState.OnPropertyUpdated -= OnGameStateChanged;
            Session.Score.OnPropertyUpdated -= OnScoreChanged;
//            Session.Stage.OnPropertyUpdated -= OnStageChanged;
            Session.Time.OnPropertyUpdated -= OnTimerChanged;
            Session.InitCooldownTime.OnPropertyUpdated -= OnGamePlayStateChanged;
            Session.Template.OnPropertyUpdated -= OnTemplateGestureChanged;
            Session.Fails.OnPropertyUpdated -= OnFailedCountChanged;
            View.OnShow.RemoveAllListeners();
            View.OnPauseBtnClick.RemoveAllListeners();
        }

        public override void OnRegister()
        {
            GameFlow.GameState.OnPropertyUpdated += OnGameStateChanged;
            Session.Score.OnPropertyUpdated += OnScoreChanged;
//            Session.Stage.OnPropertyUpdated += OnStageChanged;
            Session.Time.OnPropertyUpdated += OnTimerChanged;
            Session.InitCooldownTime.OnPropertyUpdated += OnGamePlayStateChanged;
            Session.Template.OnPropertyUpdated += OnTemplateGestureChanged;
            Session.Fails.OnPropertyUpdated += OnFailedCountChanged;
            View.OnShow.AddListener(OnShow);
            View.OnPauseBtnClick.AddListener(OnPause);
        }

        private void OnFailedCountChanged(int obj)
        {
            View.AnimRedBack();
        }

        private void OnTemplateGestureChanged(Vector2[] obj)
        {
            View.TemplateGesture = obj;
        }

        private void OnGamePlayStateChanged(TimeSpan obj)
        {
            View.InitCounter = (int) obj.TotalSeconds;
        }

        private void OnPause()
        {
            ChangeGameFlowStateSignal.Dispatch(GameStates.Pause);
        }

        private void OnTimerChanged(TimeSpan obj)
        {
            View.Time = obj;
        }

//        private void OnStageChanged(int obj)
//        {
//            View.Stage = obj;
//        }

        private void OnScoreChanged(int obj)
        {
            View.Score = obj;
        }

        private void OnShow()
        {
            View.Score = Session.Score.Value;
//            View.Stage = Session.Stage.Value;
            View.Time = Session.Time.Value;
        }

        private void OnGameStateChanged(GameStates obj)
        {
            switch (obj)
            {
                case GameStates.GamePlay:
                case GameStates.Pause:
                case GameStates.GameOver:
                    View.SetVisibility(true);
                    break;
                default:
                    View.SetVisibility(false);
                    break;
            }
        }
    }
}