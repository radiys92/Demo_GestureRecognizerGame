using GCon;
using strange.extensions.command.impl;
using UnityEngine;

public class GestureStartCommand : Command
{
    [Inject]
    public Gesture Gesture { get; private set; }

    public override void Execute()
    {
        Debug.Log("gestrue starget at " + Gesture.EndPoint);
    }
}