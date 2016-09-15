using Core.Api;
using Core.Impl;
using Logic.Commands;
using Logic.Signals;
using Model.Api;
using Model.Impl;
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
            injectionBinder.Bind<IGestureTemplatesModel>().To<GestureTemplatesModel>();
            injectionBinder.Bind<IPlaySessionModel>().To<PlaySessionModel>();

//            mediationBinder.Bind<DebugStatusBarView>().To<DebugStatusBarMediator>();

            commandBinder.Bind<AppStartSignal>().To<AppStartCommand>().To<BindInputCommand>().Once();
            commandBinder.Bind<GestureStartSignal>().To<GestureStartCommand>();
            commandBinder.Bind<GestureEndSignal>().To<GestureEndCommand>();

            commandBinder.Bind<GestureRendererCreateSignal>().To<CreateGestureRendererCommand>();
            commandBinder.Bind<GestureRendererClearSignal>().To<ClearGestureRenderersCommand>();
            commandBinder.Bind<GestureRendererUpdateSignal>().To<UpdateGestureRendererCommand>();
        }
    }
}