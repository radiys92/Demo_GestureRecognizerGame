using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class AppStartCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("App started!");
        }
    }
}