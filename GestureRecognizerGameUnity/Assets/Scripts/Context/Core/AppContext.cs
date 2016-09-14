using Logic.Commands;
using Logic.Signals;
using Model;
using UIView;
using UnityEngine;

namespace Core
{
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
            injectionBinder.Bind<IGameFlowModel>().To<GameFlowModel>().ToSingleton();

            mediationBinder.Bind<DebugStatusBarView>().To<DebugStatusBarMediator>();

            commandBinder.Bind<AppStartSignal>().To<AppStartCommand>().To<BindInputCommand>().Once();
            commandBinder.Bind<GestureStartSignal>().To<GestureStartCommand>();
            commandBinder.Bind<GestureEndSignal>().To<GestureEndCommand>();

            commandBinder.Bind<GestureRendererCreateSignal>().To<CreateGestureRendererCommand>();
            commandBinder.Bind<GestureRendererClearSignal>().To<ClearGestureRenderersCommand>();
            commandBinder.Bind<GestureRendererUpdateSignal>().To<UpdateGestureRendererCommand>();
        }
    }
}