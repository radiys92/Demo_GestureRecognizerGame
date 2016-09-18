using GCon;
using Model.Api;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class UpdateGestureRendererCommand : Command
    {
        [Inject]
        public Gesture G { get; private set; }

        [Inject]
        public IGameFlowModel Model { get; private set; }

        public override void Execute()
        {
            if (Model.LineRenderers.Count == 0)
                return;

            var lr = Model.LineRenderers[Model.LineRenderers.Count - 1];
            lr.SetVertexCount(G.FramesCount);
            var pos = Camera.main.ScreenToWorldPoint(G.EndPoint);
            pos.z = 0;
            lr.SetPosition(G.FramesCount-1, pos);
        }
    }
}