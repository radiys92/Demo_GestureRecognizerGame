using Core;
using strange.extensions.context.impl;

namespace UIView.Context
{
    public class AppContextView : ContextView
    {
        private void Awake()
        {
            context = new AppContext(this);
            context.Start();
        }
    }
}