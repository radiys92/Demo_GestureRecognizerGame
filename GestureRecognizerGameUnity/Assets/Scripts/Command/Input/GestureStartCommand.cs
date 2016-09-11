using System;
using GCon;
using strange.extensions.command.impl;

public class GestureStartCommand : Command
{
    [Inject]
    public Gesture Gesture { get; private set; }
    
    [Inject]
    public IGameSessionModel Model { get; private set; }

    [Inject]
    public GestureRendererUpdateSignal GestureRendererUpdateSignal { get; private set; }

    [Inject]
    public GestureRendererCreateSignal GestureRendererCreateSignal { get; private set; }


    public override void Execute()
    {
        Model.GameState.Value =
            (GameSessionModel.GameStates)
                (((int) Model.GameState.Value + 1)%Enum.GetNames(typeof (GameSessionModel.GameStates)).Length);

        if (Model.GameState.Value == GameSessionModel.GameStates.DrawLine1 ||
            Model.GameState.Value == GameSessionModel.GameStates.DrawLine2)
        {
            GestureRendererCreateSignal.Dispatch(Gesture);

            Gesture.OnGestureStay += g =>
            {
                GestureRendererUpdateSignal.Dispatch(Gesture);
            };
        }
    }
}