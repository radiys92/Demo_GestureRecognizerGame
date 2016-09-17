using Logic.Signals;
using Model.Api;
using Model.Impl;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class ChangeGamePlayStateCommand : Command
    {
        [Inject]
        public IGameFlowModel GameFlow { get; private set; }

        [Inject]
        public IGamePlayModel GamePlay { get; private set; }

        [Inject]
        public GamePlayState State { get; private set; }

        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; } 

        public override void Execute()
        {
            if (GameFlow.GameState.Value != GameStates.GamePlay)
                ChangeGameFlowStateSignal.Dispatch(GameStates.GamePlay);

            GamePlay.State.Value = State;

            Debug.LogFormat("GamePlay state changed to {0}", State);
        }
    }
}