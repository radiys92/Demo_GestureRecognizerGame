using System;
using UnityEngine;

namespace Helpers.Api
{
    public class LineDrawer : MonoBehaviour,
        ILineDrawer
    {
        public LineRenderer UserFigureLine;
        public LineRenderer AiFigureLine;

        private LineRenderer _lr = null;
        private int _pointsCount = 0;

        public void CreateLine(LineType lineType)
        {
            if (_lr!=null)
                DestroyLine();

            var prefab = (GameObject)null;

            switch (lineType)
            {
                case LineType.AiLine:
                    prefab = AiFigureLine.gameObject;
                    break;
                case LineType.UserLine:
                    prefab = UserFigureLine.gameObject;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lineType), lineType, null);
            }

            var go = GameObject.Instantiate(prefab);
            go.name = "~Line";
            _lr = go.GetComponent<LineRenderer>();
            _lr.SetVertexCount(0);
            _pointsCount = 0;
        }

        public void UpdateLinePoints(Vector3[] worldPoints)
        {
            if (_lr == null) return;
            _lr.SetVertexCount(worldPoints.Length);
            _lr.SetPositions(worldPoints);
            _pointsCount = worldPoints.Length;
        }

        public void DestroyLine()
        {
            if (_lr == null) return;
            Destroy(_lr.gameObject);
            _lr = null;
            _pointsCount = 0;
        }

        public void AddPoint(Vector3 worldPoint)
        {
            if (_lr == null)
                return;

            _pointsCount ++;
            _lr.SetVertexCount(_pointsCount);
            _lr.SetPosition(_pointsCount - 1, worldPoint);
        }
    }
}