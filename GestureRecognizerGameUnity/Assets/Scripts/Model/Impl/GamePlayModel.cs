using System;
using Helpers;
using Model.Api;

namespace Model.Impl
{
    public class GamePlayModel : IGamePlayModel
    {
        public HandledProperty<GamePlayState> State { get; private set; }
        public HandledProperty<int> Score { get; private set; }
        public HandledProperty<int> Stage { get; private set; }
        public HandledProperty<TimeSpan> Time { get; private set; }

        public GamePlayModel()
        {
            Score = new HandledProperty<int>(0);
            Stage = new HandledProperty<int>(0);
            Time = new HandledProperty<TimeSpan>(TimeSpan.FromSeconds(160));
            State = new HandledProperty<GamePlayState>(GamePlayState.Init);
        }
    }
}