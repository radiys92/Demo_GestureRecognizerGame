using System;
using GCon;

public interface IGestureInput
{
    event Action<Gesture> OnGestureStart;
    event Action<Gesture> OnGestureEnd;
}