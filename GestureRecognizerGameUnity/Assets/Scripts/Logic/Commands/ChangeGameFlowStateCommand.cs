using Model.Api;
using Model.Impl;
using strange.extensions.command.impl;

namespace Logic.Commands
{
    public class ChangeGameFlowStateCommand : Command
    {
        [Inject]
        public GameStates State { get; private set; }

        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        public override void Execute()
        {
            GameFlow.GameState.Value = State;
        }
    }
}