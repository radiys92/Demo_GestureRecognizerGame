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
        commandBinder.Bind<AppStartSignal>().To<AppStartCommand>().Once();
    }
}