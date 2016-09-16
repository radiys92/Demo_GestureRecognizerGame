using Model.Api;
using Model.Impl;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class RestartGamePlayCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("Restart!");
        }
    }

    public class InitGamePlayCommand : Command
    {
        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        [Inject]
        public IGamePlayModel GamePlay { get; private set; }

        public override void Execute()
        {
            if (GameFlow.GameState.Value != GameStates.GamePlay)
                GameFlow.GameState.Value = GameStates.GamePlay;

            GamePlay.State.Value = GamePlayState.Init;
        }
    }
}