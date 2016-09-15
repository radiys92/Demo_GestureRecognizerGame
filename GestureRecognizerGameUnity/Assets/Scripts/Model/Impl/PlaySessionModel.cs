using Helpers;
using Model.Api;

namespace Model.Impl
{
    public class PlaySessionModel : IPlaySessionModel
    {
        public HandledProperty<int> Score;

        public PlaySessionModel()
        {
            Score = new HandledProperty<int>(0);
        }
    }
}