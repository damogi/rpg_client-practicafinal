using System;

namespace UnityRest
{
    public class ArrayResponseParser<T>: ResponseParser
    {
        private Action<T[]> onResponse;

        public ArrayResponseParser (Action<T[]> onResponse)
        {
            this.onResponse = onResponse;
        }

        public override void OnResponse(string responseBody)
        {
            T[] elements = JsonHelper.FromJson<T> (responseBody);
            onResponse (elements);
        }
    }
}