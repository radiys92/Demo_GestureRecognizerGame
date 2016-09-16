namespace UIView
{
    public class WindowViewMediator<T> : strange.extensions.mediation.impl.Mediator
        where T : WindowView
    {
        [Inject]
        public T View { get; private set; }
    }
}