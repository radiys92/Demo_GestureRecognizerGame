using UnityEngine.Events;
using UnityEngine.UI;

namespace UIView.Windows
{
    public class PauseWindow : WindowView
    {
        public Button ResumeBtn;
        public Button RestartBtn;
        public Button GoToMainMenuBtn;
        public Text ScoreTxt;

        public int Score
        {
            set { ScoreTxt.text = string.Format("Score: {0}", value); }
        }

        public UnityEvent OnResumeBtnClick
        {
            get { return ResumeBtn.onClick; }
        }

        public UnityEvent OnrestartBtnClick
        {
            get { return RestartBtn.onClick; }
        }

        public UnityEvent OnGoToMainMenuBtnClick
        {
            get { return GoToMainMenuBtn.onClick; }
        }
    }
}