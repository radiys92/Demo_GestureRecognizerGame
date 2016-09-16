using GCon;

namespace Helpers.Api
{
    public interface IGestureRecognizer
    {
        float Compare(Gesture a, Gesture b, int maxPoints);
    }
}