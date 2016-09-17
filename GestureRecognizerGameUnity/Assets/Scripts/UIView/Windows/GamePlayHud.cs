using System;
using UnityEngine;
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

        public UnityEvent OnPauseBtnClick => PauseBtn.onClick;

        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
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
                TimeTxt.gameObject.SetActive(value > 0);
                StageTxt.gameObject.SetActive(value > 0);
                StageTxt.text = $"Stage: {value}";
                if (value > 0)
                {
                    InitCounterTxt.text = $"Stage\n{value}";
                    StartTextLabelBlink();
                }
            }
        }

        public int InitCounter
        {
            set
            {
                InitCounterTxt.text = value.ToString();
                if (value > 0)
                    StartTextLabelBlink();
            }
        }

        private void StartTextLabelBlink()
        {
            Debug.Log("Blinking started");
            InitCounterTxt.gameObject.SetActive(true);
            _animator.SetTrigger("BlinkText");
        }

        public void OnLabelBlinkingFinished()
        {
            Debug.Log("Blinking finished");
            InitCounterTxt.gameObject.SetActive(false);
        }
    }
}