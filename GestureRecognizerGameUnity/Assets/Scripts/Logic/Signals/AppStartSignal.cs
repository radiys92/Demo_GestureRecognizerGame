using GCon;
using strange.extensions.signal.impl;

namespace Logic.Signals
{
    public class AppStartSignal : Signal { }

    public class GestureStartSignal : Signal<Gesture> { }

    public class GestureEndSignal : Signal<Gesture> { }

    public class GestureRendererCreateSignal : Signal<Gesture> { }

    public class GestureRendererClearSignal : Signal { }

    public class GestureRendererUpdateSignal : Signal<Gesture> { }
}