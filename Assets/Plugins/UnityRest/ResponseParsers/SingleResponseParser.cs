using System;
using UnityEngine;

namespace UnityRest
{
    public class SingleResponseParser<T> : ResponseParser
    {
        private Action<T> onResponse;

        public SingleResponseParser (Action<T> onResponse)
        {
            this.onResponse = onResponse;
        }

        public override void OnResponse(string responseBody)
        {
            T element = JsonUtility.FromJson<T> (responseBody);
            onResponse (element);
        }
    }
}