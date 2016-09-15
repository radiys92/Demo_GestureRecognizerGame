using System;
using GCon;
using Helpers;
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
        public IGameFlowModel Model { get; private set; }

        [Inject]
        public GestureRendererClearSignal GestureRendererClearSignal { get; private set; }

        public override void Execute()
        {
//        Debug.Log("gestrue end at "+Gesture.StartPoint);

            switch (Model.GameState.Value)
            {
                case GameFlowModel.GameStates.None:
                    Model.FirstGesture.Value = null;
                    Model.SecondGesture.Value = null;
                    Model.ComparsionScore.Value = -1;
                    GestureRendererClearSignal.Dispatch();
                    break;
                case GameFlowModel.GameStates.DrawLine1:
                    Model.FirstGesture.Value = Gesture;
                    break;
                case GameFlowModel.GameStates.DrawLine2:
                    Model.SecondGesture.Value = Gesture;
                    break;
                case GameFlowModel.GameStates.Compare:
                    Model.ComparsionScore.Value = GestureRecognizer.Compare(
                        Model.FirstGesture.Value, 
                        Model.SecondGesture.Value, 
                        64);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}