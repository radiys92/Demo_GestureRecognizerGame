using GCon;
using strange.extensions.command.impl;
using UnityEngine;

public class UpdateGestureRendererCommand : Command
{
    [Inject]
    public Gesture G { get; private set; }

    [Inject]
    public IGameSessionModel Model { get; private set; }

    public override void Execute()
    {
        var lr = Model.LineRenderers[Model.LineRenderers.Count - 1];
        lr.SetVertexCount(G.FramesCount);
        var pos = Camera.main.ScreenToWorldPoint(G.EndPoint);
        pos.z = 0;
        lr.SetPosition(G.FramesCount-1, pos);
    }
}