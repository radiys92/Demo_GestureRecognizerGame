using System;
using GCon;
using strange.extensions.command.impl;

public class GestureEndCommand : Command
{
    [Inject]
    public Gesture Gesture { get; private set; }

    [Inject]
    public IGameSessionModel Model { get; private set; }

    [Inject]
    public GestureRendererClearSignal GestureRendererClearSignal { get; private set; }

    public override void Execute()
    {
//        Debug.Log("gestrue end at "+Gesture.StartPoint);

        switch (Model.GameState.Value)
        {
            case GameSessionModel.GameStates.None:
                Model.FirstGesture.Value = null;
                Model.SecondGesture.Value = null;
                Model.ComparsionScore.Value = -1;
                GestureRendererClearSignal.Dispatch();
                break;
            case GameSessionModel.GameStates.DrawLine1:
                Model.FirstGesture.Value = Gesture;
                break;
            case GameSessionModel.GameStates.DrawLine2:
                Model.SecondGesture.Value = Gesture;
                break;
            case GameSessionModel.GameStates.Compare:
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