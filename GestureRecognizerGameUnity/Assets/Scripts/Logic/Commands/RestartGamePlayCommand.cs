using Logic.Signals;
using strange.extensions.command.impl;

namespace Logic.Commands
{
    public class RestartGamePlayCommand : Command
    {
        [Inject]
        public InitGamePlaySignal InitGamePlaySignal { get; private set; }

        public override void Execute()
        {
            //            Debug.Log("Restart!");
            InitGamePlaySignal.Dispatch();
        }
    }
}