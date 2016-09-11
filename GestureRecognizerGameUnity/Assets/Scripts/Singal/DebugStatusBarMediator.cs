using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class DebugStatusBarMediator : EventMediator
{
    [Inject]
    public DebugStatusBarView View { get; private set; }

    [Inject]
    public IGameSessionModel Model { get; private set; }

    public override void OnRegister()
    {
        base.OnRegister();

        Model.GameState.OnPropertyUpdated += SetCurrentStateDescription;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        Model.GameState.OnPropertyUpdated -= SetCurrentStateDescription;
    }

    private void SetCurrentStateDescription(GameSessionModel.GameStates state)
    {
        View.UpdateText("Current game state is "+state);
    }
}