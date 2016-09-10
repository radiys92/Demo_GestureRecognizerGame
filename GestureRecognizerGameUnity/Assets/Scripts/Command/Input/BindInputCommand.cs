using GCon;
using strange.extensions.command.impl;
using UnityEngine;

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
        Debug.Log("push signal start");
        injectionBinder.GetInstance<GestureStartSignal>().Dispatch(g);
    }
    
    private void OnGestureEnd(Gesture g)
    {
        Debug.Log("push signal end");
        injectionBinder.GetInstance<GestureEndSignal>().Dispatch(g);
    }
}