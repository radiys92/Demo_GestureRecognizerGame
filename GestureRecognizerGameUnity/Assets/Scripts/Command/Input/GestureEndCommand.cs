using GCon;
using strange.extensions.command.impl;
using UnityEngine;

public class GestureEndCommand : Command
{
    [Inject]
    public Gesture Gesture { get; private set; }

    public override void Execute()
    {
        Debug.Log("gestrue end at "+Gesture.StartPoint);
    }
}