using strange.extensions.context.impl;

public class AppContextView : ContextView
{
    private void Awake()
    {
        context = new AppContext(this);
        context.Start();
    }
}