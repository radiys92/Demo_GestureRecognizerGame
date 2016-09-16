using Core;
using strange.extensions.context.impl;

namespace UIView.Context
{
    public class AppContextView : ContextView
    {
        private bool isFirstUpdate = true;

        private void Awake()
        {
            context = new AppContext(this);
        }

        void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;
                context.Start();
            }
        }
    }
}