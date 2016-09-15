using UnityEngine.Events;
using UnityEngine.UI;

namespace UIView.Windows
{
    public class MainMenuWindow : WindowView
    {
        public Button StartGameBtn;

        public UnityEvent OnGameStartBtnClick
        {
            get { return StartGameBtn.onClick; }
        }
    }
}