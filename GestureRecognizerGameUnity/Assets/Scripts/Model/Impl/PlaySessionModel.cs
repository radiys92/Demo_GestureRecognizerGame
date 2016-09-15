using Helpers;
using Model.Api;

namespace Model.Impl
{
    public class PlaySessionModel : IPlaySessionModel
    {
        public HandledProperty<int> Score { get; private set; }

        public PlaySessionModel()
        {
            Score = new HandledProperty<int>(0);
        }
    }
}