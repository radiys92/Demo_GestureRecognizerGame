using Logic.Signals;
using Model.Api;
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
            //ChangeGameFlowStateSignal.Dispatch(GameStates.MainMenu);

//            injectionBinder.GetInstance<IGamePlayModel>().Stage.Value = 1;
            injectionBinder.GetInstance<ChangeGamePlayStateSignal>().Dispatch(GamePlayState.StageStarting);
        }
    }
}