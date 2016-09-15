using UnityEngine.Events;
using UnityEngine.UI;

namespace UIView.Windows
{
    public class GameOverWindow : WindowView
    {
        public Button RestartBtn;
        public Button GoToMainMenuBtn;
        public Text ScoreTxt;

        public int Score
        {
            set { ScoreTxt.text = string.Format("Score: {0}", value); }
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