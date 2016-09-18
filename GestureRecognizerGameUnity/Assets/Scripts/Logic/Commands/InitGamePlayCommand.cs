using Logic.Signals;
using Model.Api;
using strange.extensions.command.impl;

namespace Logic.Commands
{
    public class InitGamePlayCommand : Command
    {
        [Inject]
        public ChangeGamePlayStateSignal Signal { get; private set; }

        public override void Execute()
        {
            Signal.Dispatch(GamePlayState.Init);
        }
    }
}