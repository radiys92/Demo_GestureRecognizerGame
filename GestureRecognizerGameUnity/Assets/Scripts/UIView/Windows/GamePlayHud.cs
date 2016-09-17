using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIView.Windows
{
    public class GamePlayHud : WindowView
    {
        public Button PauseBtn;
        public Text ScoreTxt;
        public Text TimeTxt;
        public Text StageTxt;
        public Text InitCounterTxt;

        public UnityEvent OnPauseBtnClick
        {
            get { return PauseBtn.onClick; }
        }

        public int Score
        {
            set { ScoreTxt.text = $"Score: {value}"; }
        }

        public TimeSpan Time
        {
            set { TimeTxt.text = $"{(int) value.TotalMinutes}:{value.Seconds}"; }
        }

        public int Stage
        {
            set
            {
                ScoreTxt.gameObject.SetActive(value > 0);
                PauseBtn.gameObject.SetActive(value > 0);
                StageTxt.gameObject.SetActive(value > 0);
                TimeTxt.gameObject.SetActive(value > 0);
                StageTxt.text = $"Stage: {value}";
            }
        }

        public int InitCounter
        {
            set
            {
                InitCounterTxt.gameObject.SetActive(value > 0);
                InitCounterTxt.text = value.ToString();
            }
        }
    }
}