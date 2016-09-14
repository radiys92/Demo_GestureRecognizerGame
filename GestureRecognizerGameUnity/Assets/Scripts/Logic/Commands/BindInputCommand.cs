using Core;
using Core.Api;
using GCon;
using Logic.Signals;
using strange.extensions.command.impl;

namespace Logic.Commands
{
    public class BindInputCommand : Command
    {
        [Inject]
        public IGestureInput _gestureInput { get; private set; }

        public override void Execute()
        {
            _gestureInput.OnGestureStart += OnGestureStart;
            _gestureInput.OnGestureEnd += OnGestureEnd;
        }


        private void OnGestureStart(Gesture g)
        {
            injectionBinder.GetInstance<GestureStartSignal>().Dispatch(g);
        }
    
        private void OnGestureEnd(Gesture g)
        {
            injectionBinder.GetInstance<GestureEndSignal>().Dispatch(g);
        }
    }
}