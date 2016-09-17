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
        public GamePlayState State { get; private set; }

        [Inject]
        public ICoroutineWorker CoroutineWorker { get; private set; }

        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; } 

        [Inject]
        public ChangeGamePlayStateSignal ChangeGamePlayStateSignal { get; private set; }

        public override void Execute()
        {
            if (GameFlow.GameState.Value != GameStates.GamePlay)
                ChangeGameFlowStateSignal.Dispatch(GameStates.GamePlay);

            GamePlay.State.Value = State;

            Debug.LogFormat("GamePlay state changed to {0}", State);

            switch (State)
            {
                case GamePlayState.None:
                    GamePlay.InitCooldownTime.Value = TimeSpan.FromSeconds(-1);
                    GamePlay.Stage.Value = -1;
                    break;
                case GamePlayState.Init:
                    CoroutineWorker.StartCoroutine(InitCoroutine());
                    break;
                case GamePlayState.StageStarting:
                    break;
                case GamePlayState.ShowTemplateGesture:
                    break;
                case GamePlayState.UserGestureInput:
                    break;
                case GamePlayState.GesturesCompare:
                    break;
                case GamePlayState.Pause:
                    break;
                case GamePlayState.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator InitCoroutine()
        {
            var secs = 3;
            while (secs>0)
            {
                GamePlay.InitCooldownTime.Value = TimeSpan.FromSeconds(secs);
                yield return new WaitForSeconds(1);
                secs--;
            }
            GamePlay.InitCooldownTime.Value = TimeSpan.FromSeconds(-1);
            GamePlay.Stage.Value = 1;
            ChangeGamePlayStateSignal.Dispatch(GamePlayState.StageStarting);
        }
    }
}