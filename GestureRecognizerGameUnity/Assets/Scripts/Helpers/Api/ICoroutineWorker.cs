using System.Collections;

namespace Helpers.Api
{
    public interface ICoroutineWorker
    {
        void StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(IEnumerator coroutine);
    }
}