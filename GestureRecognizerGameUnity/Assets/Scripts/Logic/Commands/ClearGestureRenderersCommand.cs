using Helpers.Api;
using Model.Api;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class ClearGestureRenderersCommand : Command
    {
        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        [Inject]
        public ILineDrawer LineDrawer { get; private set; }

        public override void Execute()
        {
            LineDrawer.DestroyLine();
        }
    }
}