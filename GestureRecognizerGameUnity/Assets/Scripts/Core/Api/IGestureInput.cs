using System;
using GCon;

namespace Core.Api
{
    public interface IGestureInput
    {
        event Action<Gesture> OnGestureStart;
        event Action<Gesture> OnGestureEnd;
    }
}