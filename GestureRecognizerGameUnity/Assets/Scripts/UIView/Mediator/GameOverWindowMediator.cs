using Logic.Signals;
using Model.Api;
using Model.Impl;
using UIView.Windows;

namespace UIView.Mediator
{
    public class GameOverWindowMediator : WindowViewMediator<GameOverWindow>
    {
        [Inject]
        public IGameFlowModel GameFlowModel { get; private set; }

        [Inject]
        public IPlaySessionModel PlaySession { get; private set; }

        [Inject]
        public ChangeGameFlowStateSignal ChangeGameFlowStateSignal { get; private set; }

        [Inject]
        public RestartGamePlaySignal RestartGamePlaySignal { get; private set; }

        public override void OnRemove()
        {
            GameFlowModel.GameState.OnPropertyUpdated -= OnGameStateChanged;
            PlaySession.Score.OnPropertyUpdated -= OnScoreChanged;
            View.OnrestartBtnClick.RemoveAllListeners();
            View.OnGoToMainMenuBtnClick.RemoveAllListeners();
            View.OnShow.RemoveAllListeners();
        }

        public override void OnRegister()
        {
            GameFlowModel.GameState.OnPropertyUpdated += OnGameStateChanged;
            PlaySession.Score.OnPropertyUpdated += OnScoreChanged;
            View.OnrestartBtnClick.AddListener(Restart);
            View.OnGoToMainMenuBtnClick.AddListener(GoToMainMenu);
            View.OnShow.AddListener(OnShow);
        }
        private void OnShow()
        {
            View.Score = PlaySession.Score.Value;
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
            View.SetVisibility(obj == GameStates.GameOver);
        }
    }
}