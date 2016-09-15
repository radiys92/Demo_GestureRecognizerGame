using System;
using Helpers;

namespace Model.Api
{
    public interface IPlaySessionModel
    {
        HandledProperty<int> Score { get; }
        HandledProperty<int> Stage { get; }
        HandledProperty<TimeSpan> Time { get; }
    }
}