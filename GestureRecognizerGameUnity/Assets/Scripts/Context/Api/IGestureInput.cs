using System;
using GCon;

namespace Core
{
    public interface IGestureInput
    {
        event Action<Gesture> OnGestureStart;
        event Action<Gesture> OnGestureEnd;
    }
}