using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

public class AppContextView : ContextView
{
    private void Awake()
    {
        context = new AppContext(this);
        context.Start();
    }
}