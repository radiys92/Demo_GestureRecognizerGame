using GCon;
using Helpers.Api;
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

        [Inject]
        public ILineDrawer LineDrawer { get; private set; }

        public override void Execute()
        {
            var point = Camera.main.ScreenToWorldPoint(G.EndPoint);
            point.z = 0;
            LineDrawer.AddPoint(point);
        }
    }
}