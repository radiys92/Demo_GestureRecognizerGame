using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIView.Windows
{
    public class InGameHud : WindowView
    {
        public Button PauseBtn;
        public Text ScoreTxt;
        public Text TimeTxt;
        public Text StageTxt;

        public UnityEvent OnPauseBtnClick
        {
            get { return PauseBtn.onClick; }
        }

        public int Score
        {
            set { ScoreTxt.text = string.Format("Score: {0}", value); }
        }

        public TimeSpan Time
        {
            set { TimeTxt.text = string.Format("{0}:{1}", (int) value.TotalMinutes, value.Seconds); }
        }

        public int Stage
        {
            set { StageTxt.text = string.Format("Stage: {0}", value); }
        }
    }
}