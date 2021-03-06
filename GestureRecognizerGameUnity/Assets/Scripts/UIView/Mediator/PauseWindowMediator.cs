using Logic.Signals;
using Model.Api;
using Model.Impl;
using UIView.Windows;

namespace UIView.Mediator
{
    public class PauseWindowMediator : WindowViewMediator<PauseWindow>
    {
        [Inject]
        public IGameFlowModel GameFlowModel { get; private set; }

        [Inject]
        public IGamePlayModel GamePlay { get; private set; }

        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; }

        [Inject]
        public RestartGamePlaySignal RestartGamePlaySignal { get; private set; }

        public override void OnRemove()
        {
            GameFlowModel.GameState.OnPropertyUpdated -= OnGameStateChanged;
            GamePlay.Score.OnPropertyUpdated -= OnScoreChanged;
            View.OnResumeBtnClick.RemoveAllListeners();
            View.OnrestartBtnClick.RemoveAllListeners();
            View.OnGoToMainMenuBtnClick.RemoveAllListeners();
            View.OnShow.RemoveAllListeners();
        }

        public override void OnRegister()
        {
            GameFlowModel.GameState.OnPropertyUpdated += OnGameStateChanged;
            GamePlay.Score.OnPropertyUpdated += OnScoreChanged;
            View.OnResumeBtnClick.AddListener(Resume);
            View.OnrestartBtnClick.AddListener(Restart);
            View.OnGoToMainMenuBtnClick.AddListener(GoToMainMenu);
            View.OnShow.AddListener(OnShow);
        }

        private void OnShow()
        {
            View.Score = GamePlay.Score.Value;
        }

        private void GoToMainMenu()
        {
            ChangeGameFlowStateSignal.Dispatch(GameStates.MainMenu);
        }

        private void Restart()
        {
            Resume();
            RestartGamePlaySignal.Dispatch();
        }

        private void Resume()
        {
            ChangeGameFlowStateSignal.Dispatch(GameStates.GamePlay);
        }

        private void OnScoreChanged(int score)
        {
            if (View.IsVisible)
                View.Score = score;
        }

        private void OnGameStateChanged(GameStates obj)
        {
            View.SetVisibility(obj == GameStates.Pause);
        }
    }
}