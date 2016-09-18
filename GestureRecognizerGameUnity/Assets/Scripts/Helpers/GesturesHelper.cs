using System.Linq;
using UnityEngine;

namespace Helpers
{
    public static class GesturesHelper
    {
        public static Vector2[] NormalizeToRect(Canvas ownerCanvas, RectTransform contentRect, Vector2[] points)
        {
            // some magic here
            var rect = RectTransformToScreenSpace(contentRect, ownerCanvas);
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
                offset.y = rect.height/2 + h/2;
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
            var corners = new Vector3[4];
            var screenCorners = new Vector3[2];

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

        public static Vector3[] ScreenToWorldPoints(Camera cam, Vector2[] points)
        {
            return points.Select(i => cam.ScreenToWorldPoint(i)).Select(i =>
            {
                i.z = 0;
                return i;
            }).ToArray();
        }
    }
}