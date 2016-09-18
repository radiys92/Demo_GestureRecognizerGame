using System;
using GCon;
using Helpers;
using Helpers.Api;
using Logic.Signals;
using Model.Api;
using Model.Impl;
using strange.extensions.command.impl;

namespace Logic.Commands
{
    public class GestureEndCommand : Command
    {
        [Inject]
        public Gesture Gesture { get; private set; }

        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        [Inject]
        public IGamePlayModel GamePlay { get; private set; }

        [Inject]
        public IGestureRecognizer GestureRecognizer { get; private set; }

        [Inject]
        public GestureRendererClearSignal GestureRendererClearSignal { get; private set; }

        [Inject]
        public ChangeGamePlayStateSignal ChangeGamePlayStateSignal { get; private set; }

        public override void Execute()
        {
            GestureRendererClearSignal.Dispatch();

            if (GamePlay.State.Value != GamePlayState.UserGestureInput)
                return;

            var comparationRate = GestureRecognizer.Compare(GamePlay.Template.Value, Gesture, 64);
            if (comparationRate > 0.8f)
            {
                GamePlay.Score.Value++;
                ChangeGamePlayStateSignal.Dispatch(GamePlayState.ShowTemplateGesture);
            }
            else
            {
                GamePlay.Fails.Value++;
            }

            //            switch (GameFlow.GameState.Value)
            //            {
            //                case GameStates.None:
            //                    GameFlow.FirstGesture.Value = null;
            //                    GameFlow.SecondGesture.Value = null;
            //                    GameFlow.ComparsionScore.Value = -1;
            //                    GestureRendererClearSignal.Dispatch();
            //                    break;
            //                case GameStates.DrawLine1:
            //                    GameFlow.FirstGesture.Value = Gesture;
            //                    break;
            //                case GameStates.DrawLine2:
            //                    GameFlow.SecondGesture.Value = Gesture;
            //                    break;
            //                case GameStates.Compare:
            //                    GameFlow.ComparsionScore.Value = GestureRecognizer.Compare(
            //                        GameFlow.FirstGesture.Value, 
            //                        GameFlow.SecondGesture.Value, 
            //                        64);
            //                    break;
            //                default:
            //                    throw new ArgumentOutOfRangeException();
        }
    }
}