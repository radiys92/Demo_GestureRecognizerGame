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
        public IGameFlowModel GameFlow { get; private set; }

        [Inject]
        public IGamePlayModel GamePlay { get; private set; }

        [Inject]
        public GestureRendererUpdateSignal GestureRendererUpdateSignal { get; private set; }

        [Inject]
        public GestureRendererCreateSignal GestureRendererCreateSignal { get; private set; }


        public override void Execute()
        {
            if (GamePlay.CurrentGestureId != -1)
                return;

            GamePlay.CurrentGestureId = Gesture.ID;

            if (GamePlay.State.Value != GamePlayState.UserGestureInput)
                return;

            GestureRendererCreateSignal.Dispatch(Gesture);

            Gesture.OnGestureStay += g =>
            {
                GestureRendererUpdateSignal.Dispatch(Gesture);
            };
        }
    }
}