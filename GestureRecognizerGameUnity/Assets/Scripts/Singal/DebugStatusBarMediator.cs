using System;
using GCon;
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
        Model.FirstGesture.OnPropertyUpdated += OnFirstGestureSetted;
        Model.SecondGesture.OnPropertyUpdated += OnSecondGestureSetted;
        Model.ComparsionScore.OnPropertyUpdated += OnComparsionScoreSetted;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        Model.GameState.OnPropertyUpdated -= SetCurrentStateDescription;
        Model.FirstGesture.OnPropertyUpdated -= OnFirstGestureSetted;
        Model.SecondGesture.OnPropertyUpdated -= OnSecondGestureSetted;
        Model.ComparsionScore.OnPropertyUpdated -= OnComparsionScoreSetted;
    }

    private void SetCurrentStateDescription(GameSessionModel.GameStates state)
    {
        switch (state)
        {
            case GameSessionModel.GameStates.None:
                View.UpdateText("Start draging to draw first gesture");
                break;
            case GameSessionModel.GameStates.DrawLine1:
                View.UpdateText("Drawing gesture 1");
                break;
            case GameSessionModel.GameStates.DrawLine2:
                View.UpdateText("Drawing gesture 2");
                break;
            case GameSessionModel.GameStates.Compare:
                View.UpdateText("Comparing...");
                break;
            default:
                throw new ArgumentOutOfRangeException("state", state, null);
        }
    }

    private void OnComparsionScoreSetted(float score)
    {
        if (score <= 0) return;

        if (score > 0.9) View.UpdateText("Great! Three stars!");
        else if (score > 0.8) View.UpdateText("You can do better, but this is normal... Go forward.");
        else View.UpdateText("Level failed");
    }

    private void OnSecondGestureSetted(Gesture g)
    {
        if (g == null) return;

        View.UpdateText("Click for compare!");
    }

    private void OnFirstGestureSetted(Gesture g)
    {
        if (g == null) return;

        View.UpdateText("Enter second gesture!");
    }
}