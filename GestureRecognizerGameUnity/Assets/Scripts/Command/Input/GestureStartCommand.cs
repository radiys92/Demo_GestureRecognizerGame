using System;
using GCon;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine;

public class GestureStartCommand : Command
{
    [Inject]
    public Gesture Gesture { get; private set; }
    
    [Inject]
    public IGameSessionModel model { get; private set; }

    public override void Execute()
    {
        Debug.Log("gestrue starget at " + Gesture.EndPoint);

        model.GameState.Value =
            (GameSessionModel.GameStates)
                (((int) model.GameState.Value + 1)%Enum.GetNames(typeof (GameSessionModel.GameStates)).Length);

//        dispatcher.Dispatch(Events.Game.GameStateChanged, evt.data);
    }
}