using System;
using GCon;
using Logic.Signals;
using Model.Api;
using Model.Impl;
using strange.extensions.command.impl;

namespace Logic.Commands
{
    public class GestureStartCommand : Command
    {
        [Inject]
        public Gesture Gesture { get; private set; }
    
        [Inject]
        public IGameFlowModel Model { get; private set; }

        [Inject]
        public GestureRendererUpdateSignal GestureRendererUpdateSignal { get; private set; }

        [Inject]
        public GestureRendererCreateSignal GestureRendererCreateSignal { get; private set; }


        public override void Execute()
        {
            Model.GameState.Value =
                (GameStates)
                    (((int) Model.GameState.Value + 1)%Enum.GetNames(typeof (GameStates)).Length);

            if (Model.GameState.Value == GameStates.DrawLine1 ||
                Model.GameState.Value == GameStates.DrawLine2)
            {
                GestureRendererCreateSignal.Dispatch(Gesture);

                Gesture.OnGestureStay += g =>
                {
                    GestureRendererUpdateSignal.Dispatch(Gesture);
                };
            }
        }
    }
}