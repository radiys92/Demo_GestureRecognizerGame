using GCon;
using strange.extensions.signal.impl;

public class AppStartSignal : Signal { }

public class GestureStartSignal : Signal<Gesture> { }
public class GestureEndSignal : Signal<Gesture> { }