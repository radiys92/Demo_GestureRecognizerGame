using Logic.Signals;
using Model.Impl;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class AppStartCommand : Command
    {
        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; }

        public override void Execute()
        {
            Debug.Log("App started!");
            ChangeGameFlowStateSignal.Dispatch(GameStates.MainMenu);
        }
    }
}