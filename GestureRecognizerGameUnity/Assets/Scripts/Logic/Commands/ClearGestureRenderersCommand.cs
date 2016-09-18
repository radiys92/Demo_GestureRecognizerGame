using Model.Api;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class ClearGestureRenderersCommand : Command
    {
        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        public override void Execute()
        {
            foreach (var lineRenderer in GameFlow.LineRenderers)
            {
                Object.Destroy(lineRenderer.gameObject);
            }
            GameFlow.LineRenderers.Clear();
        }
    }
}