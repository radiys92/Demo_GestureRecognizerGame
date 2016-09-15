using Model.Api;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class ClearGestureRenderersCommand : Command
    {
        [Inject]
        public IGameFlowModel Model { get; private set; }

        public override void Execute()
        {
            foreach (var lineRenderer in Model.LineRenderers)
            {
                Object.Destroy(lineRenderer.gameObject);
            }
            Model.LineRenderers.Clear();
        }
    }
}