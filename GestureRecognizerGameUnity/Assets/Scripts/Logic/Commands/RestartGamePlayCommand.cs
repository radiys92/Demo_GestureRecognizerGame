using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class RestartGamePlayCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("Restart!");
        }
    }
}