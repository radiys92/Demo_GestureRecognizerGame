using strange.extensions.command.impl;
using UnityEngine;

public class ClearGestureRenderersCommand : Command
{
    [Inject]
    public IGameSessionModel Model { get; private set; }

    public override void Execute()
    {
        foreach (var lineRenderer in Model.LineRenderers)
        {
            Object.Destroy(lineRenderer.gameObject);
        }
        Model.LineRenderers.Clear();
    }
}