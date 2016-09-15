using System;
using Helpers;
using Model.Api;

namespace Model.Impl
{
    public class PlaySessionModel : IPlaySessionModel
    {
        public HandledProperty<int> Score { get; private set; }
        public HandledProperty<int> Stage { get; private set; }
        public HandledProperty<TimeSpan> Time { get; private set; }

        public PlaySessionModel()
        {
            Score = new HandledProperty<int>(0);
            Stage = new HandledProperty<int>(0);
            Time = new HandledProperty<TimeSpan>(TimeSpan.FromSeconds(160));
        }
    }
}