using strange.extensions.context.impl;

public class AppContextView : ContextView
{
    void Awake()
    {
        context = new AppContext(this);
        context.Start();
    }
}