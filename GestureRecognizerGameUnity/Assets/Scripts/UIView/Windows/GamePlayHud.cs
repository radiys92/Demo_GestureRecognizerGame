using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public RectTransform ContentRect;
        public LineRenderer GestureRenderer;
        public Canvas MyCanvas;

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
                    BlinkText($"Stage\n{value}");
                }
            }
        }

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
            var points = NormalizeToRect(ContentRect, gesturePoints);
            var worldPoints = ScreenToWorldPoints(points);
//            var worldPoints = ScreenToWorldPoints(gesturePoints);
            StartCoroutine(DrawPoints(worldPoints, 1, 1));
        }

        private Vector3[] ScreenToWorldPoints(Vector2[] points)
        {
            return points.Select(i => Camera.main.ScreenToWorldPoint(i)).Select(i =>
            {
                i.z = 0;
                return i;
            }).ToArray();
        }

        private IEnumerator DrawPoints(Vector3[] points, int drawTime, int hideTime)
        {
            var startTime = UnityEngine.Time.time;
            for (var drawDelta = 0f; drawDelta < drawTime; drawDelta = UnityEngine.Time.time - startTime)
            {
                var neededPointsCount = (int) ((drawDelta/drawTime)*points.Length);
                SetPoints(GestureRenderer, points, 0, neededPointsCount);
                yield return new WaitForEndOfFrame();
            }
            SetPoints(GestureRenderer, points, 0, points.Length);
            yield return new WaitForSeconds(hideTime);
            GestureRenderer.SetVertexCount(0);
        }

        private void SetPoints(LineRenderer lineRenderer, Vector3[] points, int start, int count)
        {
            lineRenderer.SetVertexCount(count);
            lineRenderer.SetPositions(points.Skip(start).Take(count).ToArray());
        }

        private Vector2[] NormalizeToRect(RectTransform contentRect, Vector2[] points)
        {
            // some magic here
            var rect = RectTransformToScreenSpace(contentRect, MyCanvas);
            rect.xMin += 50;
            rect.xMax -= 50;
            rect.yMin += 50;
            rect.yMax -= 50;
            var left = points.Min(i => i.x);
            var right = points.Max(i => i.x);
            var bot = points.Min(i => i.y);
            var top = points.Max(i => i.y);
            var h = top - bot;
            var w = right - left;
            var offset = new Vector2(Screen.width / 2 - left, Screen.height / 2 - bot);
            var scale = new Vector2(rect.width / w, rect.height / h);
            return
                points.Select(
                    i => new Vector2()
                    {
                        x =  (i.x - left)*scale.x,
                        y = offset.y + (i.y - bot)*scale.y
                    })
                    .ToArray();
        }

        private static Rect RectTransformToScreenSpace(RectTransform rectTransform, Canvas canvas)
        {
            Vector3[] corners = new Vector3[4];
            Vector3[] screenCorners = new Vector3[2];

            rectTransform.GetWorldCorners(corners);

            if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
            {
                screenCorners[0] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[1]);
                screenCorners[1] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[3]);
            }
            else
            {
                screenCorners[0] = RectTransformUtility.WorldToScreenPoint(null, corners[1]);
                screenCorners[1] = RectTransformUtility.WorldToScreenPoint(null, corners[3]);
            }

            screenCorners[0].y = Screen.height - screenCorners[0].y;
            screenCorners[1].y = Screen.height - screenCorners[1].y;

            return new Rect(screenCorners[0], screenCorners[1] - screenCorners[0]);
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
    }
}