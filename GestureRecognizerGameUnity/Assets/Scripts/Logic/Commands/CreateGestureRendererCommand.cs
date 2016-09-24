using GCon;
using Helpers.Api;
using Model.Api;
using strange.extensions.command.impl;

namespace Logic.Commands
{
    public class CreateGestureRendererCommand : Command
    {
        [Inject]
        public Gesture G { get; private set; }

        [Inject]
        public IGamePlayModel Model { get; private set; }

        [Inject]
        public ILineDrawer LineDrawer { get; private set; }

        public override void Execute()
        {
            LineDrawer.CreateLine(Model.State.Value == GamePlayState.UserGestureInput
                ? LineType.UserLine
                : LineType.AiLine);
        }

        
    }
}