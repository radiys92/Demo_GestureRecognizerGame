using System;
using Helpers;
using Model.Api;

namespace Model.Impl
{
    public class GamePlayModel : IGamePlayModel
    {
        public HandledProperty<GamePlayState> State { get; } = new HandledProperty<GamePlayState>(GamePlayState.None);
        public HandledProperty<int> Score { get; } = new HandledProperty<int>(0);
        public HandledProperty<int> Stage { get; } = new HandledProperty<int>(0);
        public HandledProperty<TimeSpan> Time { get; } = new HandledProperty<TimeSpan>(TimeSpan.FromSeconds(0));
    }
}