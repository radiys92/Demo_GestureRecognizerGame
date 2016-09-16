using Logic.Signals;
using Model.Api;
using Model.Impl;
using UIView.Windows;
using UnityEngine;

namespace UIView.Mediator
{
    public class MainMenuWindowMediator : WindowViewMediator<MainMenuWindow>
    {
        [Inject]
        public IGameFlowModel GameFlowModel { get; private set; }

        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; }

        public override void OnRegister()
        {
            GameFlowModel.GameState.OnPropertyUpdated += OnGameStateUpdated;
            View.OnGameStartBtnClick.AddListener(OnStartGame);
        }

        public override void OnRemove()
        {
            GameFlowModel.GameState.OnPropertyUpdated -= OnGameStateUpdated;
            View.OnGameStartBtnClick.RemoveAllListeners();
        }

        private void OnGameStateUpdated(GameStates obj)
        {
            View.SetVisibility(obj == GameStates.MainMenu);
        }
        private void OnStartGame()
        {
            ChangeGameFlowStateSignal.Dispatch(GameStates.GamePlay);
        }
    }
}