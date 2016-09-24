using System;
using System.Collections;
using System.Linq;
using Helpers;
using Helpers.Api;
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
        public RectTransform GestureRendererRect;
        public Canvas MyCanvas;

        [Inject]
        public ILineDrawer LineDrawer { get; private set; }

        public UnityEvent OnPauseBtnClick => PauseBtn.onClick;

        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();
        }

        public int Score
        {
            set
            {
                ScoreTxt.text = $"Score: {value}";
                if (value > 0)
                    AnimGreenBack();
            }
        }

        public TimeSpan Time
        {
            set { TimeTxt.text = $"{(int)(value.TotalSeconds)}.{value.Milliseconds.ToString("000")}"; }
        }

//        public int Stage
//        {
//            set
//            {
//                ScoreTxt.gameObject.SetActive(value > 0);
//                PauseBtn.gameObject.SetActive(value > 0);
//                TimeTxt.gameObject.SetActive(value > 0);
//                StageTxt.gameObject.SetActive(value > 0);
//                StageTxt.text = $"Stage: {value}";
//                if (value > 0)
//                {
//                    BlinkText($"Stage\n{value}");
//                }
//            }
//        }

        public int InitCounter
        {
            set
            {
                if (value > 0)
                    BlinkText(value.ToString());
            }
        }

        public Vector2[] TemplateGesture
        {
            set { DrawTemplate(value); }
        }

        private void DrawTemplate(Vector2[] gesturePoints)
        {
            var points = GesturesHelper.NormalizeToRect(MyCanvas, GestureRendererRect, gesturePoints);
            var worldPoints = GesturesHelper.ScreenToWorldPoints(Camera.main, points);
            StartCoroutine(DrawPoints(worldPoints, 1, 1));
        }

        

        private IEnumerator DrawPoints(Vector3[] points, int drawTime, int hideTime)
        {
            LineDrawer.CreateLine(LineType.AiLine);
            var startTime = UnityEngine.Time.time;
            for (var drawDelta = 0f; drawDelta < drawTime; drawDelta = UnityEngine.Time.time - startTime)
            {
                var neededPointsCount = (int) ((drawDelta/drawTime)*points.Length);
                SetPoints(points, 0, neededPointsCount);
                yield return new WaitForEndOfFrame();
            }
            SetPoints(points, 0, points.Length);
            yield return new WaitForSeconds(hideTime);
            LineDrawer.DestroyLine();
        }

        private void SetPoints(Vector3[] points, int start, int count)
        {
            LineDrawer.UpdateLinePoints(points.Skip(start).Take(count).ToArray());
        }

        private void BlinkText(string text)
        {
            Debug.Log("Blinking started");
            InitCounterTxt.text = text;
            InitCounterTxt.gameObject.SetActive(true);
            _animator.SetTrigger("BlinkText");
        }

        public void OnLabelBlinkingFinished()
        {
            Debug.Log("Blinking finished");
            InitCounterTxt.gameObject.SetActive(false);
        }

        public void AnimGreenBack()
        {
            _animator.SetTrigger("GreenBack");
        }

        public void AnimRedBack()
        {
            _animator.SetTrigger("RedBack");
        }
    }
}