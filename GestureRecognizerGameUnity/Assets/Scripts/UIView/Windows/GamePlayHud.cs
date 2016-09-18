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
        public RectTransform GestureRendererRect;
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
            var points = NormalizeToRect(GestureRendererRect, gesturePoints);
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
//            GestureRenderer.SetVertexCount(0);
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
//            rect.xMin += 50;
//            rect.xMax -= 50;
//            rect.yMin += 50;
//            rect.yMax -= 50;
            var left = float.MaxValue;
            var right = float.MinValue;
            var bot = float.MaxValue;
            var top = float.MinValue;
            for (var i = 0; i < points.Length; i++)
            {
                if (points[i].x <= left) left = points[i].x;
                if (points[i].x >= right) right = points[i].x;
                if (points[i].y <= bot) bot = points[i].y;
                if (points[i].y >= top) top = points[i].y;
            }
            var topLeft = new Vector2(left, top);
            var h = top - bot;
            var w = right - left;
            var offset = Vector2.zero;
            var scale = 0f;
            var scaleW = rect.width/w;
            var scaleH = rect.height/h;
            if (scaleH < scaleW)
            {
//                Debug.Log("vertical figures");
                scale = scaleH;
                offset.y = rect.height;
                offset.x = rect.width/2 - w;
            }
            else
            {
//                Debug.Log("horizontal figures");
                scale = scaleW;
                offset.y = rect.height/2+h/2;
//                offset.x = 0;

            }
            var res = points.Select(
                i =>
                {
                    var v = i - topLeft;
                    v *= scale;
                    v += offset;

                    v.y += rect.yMin;

                    return v;

                })
                    .ToArray();

//            Debug.LogFormat("Offset = {0}", offset);
//            Debug.LogFormat("{0}\n{1}\n{2}\n{3}",left,right,bot,top);
////            var res = points.Select(i => Vector2.zero).ToArray();
//
//            var start = ScreenToWorldPoints(points);
//            var fin = ScreenToWorldPoints(res);
//            for (var i = 0; i < start.Length; i++)
//            {
//                if (i > 0)
//                {
//                    Debug.DrawLine(start[i-1],start[i], Color.green);
//                    Debug.DrawLine(fin[i-1],fin[i], Color.green);
//                }
//                Debug.DrawLine(start[i],fin[i], Color.cyan);
//            }
//
//            var startCorners = ScreenToWorldPoints(new[] {rect.min, rect.max});
//            var lt = new Vector2(startCorners[0].x, startCorners[1].y);
//            var lb = new Vector2(startCorners[0].x, startCorners[0].y);
//            var rt = new Vector2(startCorners[1].x, startCorners[1].y);
//            var rb = new Vector2(startCorners[1].x, startCorners[0].y);
//            Debug.DrawLine(lt, lb, Color.red);
//            Debug.DrawLine(lt, rt, Color.red);
//            Debug.DrawLine(rt, rb, Color.red);
//            Debug.DrawLine(lb, rb, Color.red);
//            var finCorners = ScreenToWorldPoints(new[] {new Vector2(left, top), new Vector2(right, bot) });
//            lt = new Vector2(finCorners[0].x, finCorners[1].y);
//            lb = new Vector2(finCorners[0].x, finCorners[0].y);
//            rt = new Vector2(finCorners[1].x, finCorners[1].y);
//            rb = new Vector2(finCorners[1].x, finCorners[0].y);
//            Debug.DrawLine(lt, lb, Color.gray);
//            Debug.DrawLine(lt, rt, Color.gray);
//            Debug.DrawLine(rt, rb, Color.gray);
//            Debug.DrawLine(lb, rb, Color.gray);
//            Debug.Break();

            return res;

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