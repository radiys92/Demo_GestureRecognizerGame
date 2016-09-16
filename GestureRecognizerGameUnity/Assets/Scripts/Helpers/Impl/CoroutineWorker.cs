using System.Collections;
using Helpers.Api;
using UnityEngine;

namespace Helpers.Impl
{
    public class CoroutineWorker : MonoBehaviour, ICoroutineWorker 
    {
        public new void StartCoroutine(IEnumerator coroutine)
        {
            base.StartCoroutine(coroutine);
        }

        public new void StopCoroutine(IEnumerator coroutine)
        {
            base.StopCoroutine(coroutine);
        }
    }
}