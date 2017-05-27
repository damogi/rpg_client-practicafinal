using System.Collections;

namespace UnityRest
{
    public interface ICoroutineExecuter
    {
        void StartCoroutine (IEnumerator coroutine);
    }
}