using GCon;
using UnityEngine;

namespace Helpers.Api
{
    public interface IGestureRecognizer
    {
        float Compare(Gesture gesture1, Gesture gesture2, int maxPoints);
        float Compare(Vector2[] gesture1Points, Gesture gesture2, int maxPoints);
    }
}