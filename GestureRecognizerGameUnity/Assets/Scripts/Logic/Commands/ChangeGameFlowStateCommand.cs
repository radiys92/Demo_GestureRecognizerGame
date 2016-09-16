using Model.Api;
using Model.Impl;
using strange.extensions.command.impl;
using UnityEngine;

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
            Debug.LogFormat("Changing game state to {0}",State);
            GameFlow.GameState.Value = State;
        }
    }
}