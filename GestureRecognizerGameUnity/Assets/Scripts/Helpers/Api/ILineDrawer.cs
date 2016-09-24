using UnityEngine;

namespace Helpers.Api
{
    public interface ILineDrawer
    {
        void CreateLine(LineType lineType);
        void UpdateLinePoints(Vector3[] worldPoints);
        void DestroyLine();
        void AddPoint(Vector3 worldPoint);
    }
}