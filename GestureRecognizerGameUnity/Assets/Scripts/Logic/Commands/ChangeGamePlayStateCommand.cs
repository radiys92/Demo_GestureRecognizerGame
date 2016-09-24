using System;
using System.Collections;
using Helpers.Api;
using Logic.Signals;
using Model.Api;
using Model.Impl;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class ChangeGamePlayStateCommand : Command
    {
        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        [Inject]
        public IGamePlayModel GamePlay { get; private set; }

        [Inject]
        public IGestureTemplatesModel Templates { get; private set; }

        [Inject]
        public GamePlayState State { get; private set; }

        [Inject]
        public ICoroutineWorker CoroutineWorker { get; private set; }

        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; }

        [Inject]
        public ChangeGamePlayStateSignal ChangeGamePlayStateSignal { get; private set; }

        [Inject]
        public GestureRendererClearSignal GestureRendererClearSignal { get; private set; }

        private static IEnumerator _gestureInputWaiter = null;

        public override void Execute()
        {
            if (GameFlow.GameState.Value != GameStates.GamePlay && State != GamePlayState.None)
                ChangeGameFlowStateSignal.Dispatch(GameStates.GamePlay);

            GamePlay.State.Value = State;

            Debug.LogFormat("GamePlay state changed to {0}", State);

            switch (State)
            {
                case GamePlayState.None:
                    StopGestureWaiter();
                    WipeSessionData();
                    break;
                case GamePlayState.Init:
                    WipeSessionData();
                    CoroutineWorker.StartCoroutine(InitCoroutine());
                    break;
//                case GamePlayState.StageStarting:
//                    var stage = GamePlay.Stage.Value <= 0
//                        ? 1
//                        : GamePlay.Stage.Value + 1;
//                    CoroutineWorker.StartCoroutine(StartStage(stage));
//                    break;
                case GamePlayState.ShowTemplateGesture:
                    GamePlay.Time.Value = TimeSpan.FromSeconds(GamePlay.CurrentCooldown.Value);
                    StopGestureWaiter();
                    var template = GetRandomTemplate();
                    CoroutineWorker.StartCoroutine(DrawTemplateGestureState(template));
                    break;
                case GamePlayState.UserGestureInput:
                    StartGestureWaiter();
                    break;
//                case GamePlayState.GesturesCompare:
//                    break;
                case GamePlayState.Pause:
                    Time.timeScale = 0;
                    break;
                case GamePlayState.GameOver:
                    ChangeGameFlowStateSignal.Dispatch(GameStates.GameOver);
                    GamePlay.State.Value = GamePlayState.None;
                    GestureRendererClearSignal.Dispatch();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WipeSessionData()
        {
            Time.timeScale = 1;
            GamePlay.InitCooldownTime.Value = TimeSpan.FromSeconds(0);
            GamePlay.Score.Value = 0;
            GamePlay.Time.Value = TimeSpan.FromSeconds(0);
            GamePlay.CurrentCooldown.Value = 20;
            //                    GamePlay.Stage.Value = -1;
        }

        private void StartGestureWaiter()
        {
            StopGestureWaiter();
            _gestureInputWaiter = UserGestureInputStage();
            CoroutineWorker.StartCoroutine(_gestureInputWaiter);
        }

        private void StopGestureWaiter()
        {
            if (_gestureInputWaiter != null)
            {
                CoroutineWorker.StopCoroutine(_gestureInputWaiter);
                _gestureInputWaiter = null;
            }
        }

        private IEnumerator UserGestureInputStage()
        {
            var startTime = Time.time;
            for (var delta = 0f; delta < GamePlay.CurrentCooldown.Value; delta = Time.time - startTime)
            {
                GamePlay.Time.Value = TimeSpan.FromSeconds(GamePlay.CurrentCooldown.Value - delta);
                yield return new WaitForEndOfFrame();
            }
            ChangeGamePlayStateSignal.Dispatch(GamePlayState.GameOver);
        }

        private IEnumerator DrawTemplateGestureState(Vector2[] template)
        {
            GamePlay.Template.Value = template;
            yield return new WaitForSeconds(2);
            ChangeGamePlayStateSignal.Dispatch(GamePlayState.UserGestureInput);
        }

        private Vector2[] GetRandomTemplate()
        {
            return Templates.GestureTemplates[UnityEngine.Random.Range(0, Templates.GestureTemplates.Count)];
//            return Templates.GestureTemplates[0];
        }

//        private IEnumerator StartStage(int stage)
//        {
//            GamePlay.Stage.Value = stage;
//            yield return new WaitForSeconds(1);
//            ChangeGamePlayStateSignal.Dispatch(GamePlayState.ShowTemplateGesture);
//        }

        private IEnumerator InitCoroutine()
        {
            var secs = 3;
            while (secs > 0)
            {
                GamePlay.InitCooldownTime.Value = TimeSpan.FromSeconds(secs);
                yield return new WaitForSeconds(1);
                secs--;
            }
            GamePlay.InitCooldownTime.Value = TimeSpan.FromSeconds(-1);
            //            ChangeGamePlayStateSignal.Dispatch(GamePlayState.StageStarting);
            ChangeGamePlayStateSignal.Dispatch(GamePlayState.ShowTemplateGesture);
        }
    }
}