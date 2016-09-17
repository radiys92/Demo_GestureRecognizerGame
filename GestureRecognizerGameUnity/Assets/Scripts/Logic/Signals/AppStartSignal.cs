using GCon;
using Model.Api;
using Model.Impl;
using strange.extensions.signal.impl;

namespace Logic.Signals
{
    public class AppStartSignal : Signal { }


    public class GestureStartSignal : Signal<Gesture> { }

    public class GestureEndSignal : Signal<Gesture> { }

    public class GestureRendererCreateSignal : Signal<Gesture> { }

    public class GestureRendererClearSignal : Signal { }

    public class GestureRendererUpdateSignal : Signal<Gesture> { }



    public class ChangeGameFlowStateSignal : Signal<GameStates> { }

    public class RestartGamePlaySignal : Signal { }

    public class InitGamePlaySignal : Signal { }

    public class ChangeGamePlayStateSignal: Signal<GamePlayState> { }
}