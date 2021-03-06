using System;
using Core.Api;
using GCon;

namespace Core.Impl
{
    public class GestureInputContext : IGestureInput
    {
        public GestureInputContext()
        {
            GestureController.OnGestureStart = g =>
            {
                if (OnGestureStart != null)
                    OnGestureStart(g);
            };

            GestureController.OnGestureEnd = g =>
            {
                if (OnGestureEnd != null)
                    OnGestureEnd(g);
            };
        }

        public event Action<Gesture> OnGestureStart;
        public event Action<Gesture> OnGestureEnd;
    }
}