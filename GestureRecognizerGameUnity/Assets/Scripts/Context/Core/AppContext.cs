using UnityEngine;

public class AppContext : SignalContext
{
    public AppContext(MonoBehaviour view) : base(view)
    {
    }

    public override void Launch()
    {
        base.Launch();
        injectionBinder.GetInstance<AppStartSignal>().Dispatch();
    }

    protected override void mapBindings()
    {
        base.mapBindings();

        injectionBinder.Bind<IGestureInput>().To<GestureInputContext>().ToSingleton();

        commandBinder.Bind<AppStartSignal>().To<AppStartCommand>().To<BindInputCommand>().Once();
        commandBinder.Bind<GestureStartSignal>().To<GestureStartCommand>();
        commandBinder.Bind<GestureEndSignal>().To<GestureEndCommand>();
    }
}