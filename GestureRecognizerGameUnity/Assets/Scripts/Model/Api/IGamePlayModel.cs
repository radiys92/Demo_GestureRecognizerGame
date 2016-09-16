using System;
using Helpers;

namespace Model.Api
{
    public interface IGamePlayModel
    {
        HandledProperty<int> Score { get; }
        HandledProperty<int> Stage { get; }
        HandledProperty<TimeSpan> Time { get; }
    }
}