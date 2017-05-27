using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityRest
{
    public class UnityRestRequest
    {
        private Dictionary<string, string> headers;
        private string baseUrl;
        private string endpoint;
        private ObjectId id;
        private Dictionary<string, string> body;
        private HttpVerb verb;
        private ResponseParser responseHandler;
        private Action onResult;
        private Action<string> onError;
        private ICoroutineExecuter executer;

        public UnityRestRequest (HttpVerb verb, string baseUrl, string endpoint, ICoroutineExecuter executer)
        {
            this.verb = verb;
            this.baseUrl = baseUrl;
            this.endpoint = endpoint;
            this.executer = executer;
        }

        public UnityRestRequest WithBody (string body)
        {
            body = body.Trim('{', '}');

            string[] bodySplit = body.Split(',');

            Dictionary<string, string> values = new Dictionary<string, string>();

            for (int i = 0; i < bodySplit.Length; i++)
            {
                string key = bodySplit[i].Split(':')[0];
                string onevalue = bodySplit[i].Split(':')[1];

                key = key.Replace('"', ' ');
                key = key.Trim();

                onevalue = onevalue.Replace('"', ' ');
                onevalue = onevalue.Trim();

                values.Add(key, onevalue);
            }

            this.body = values;

            return this;
        }

        public UnityRestRequest WithBody<T> (T body)
        {
            string json = JsonUtility.ToJson (body);
            return WithBody (json);
        }

        public UnityRestRequest WithId (ObjectId id)
        {
            this.id = id;
            return this;
        }

        public UnityRestRequest WithHeaders (Dictionary<string, string> headers)
        {
            this.headers = headers;
            return this;
        }

        public UnityRestRequest OnError (Action<string> onError)
        {
            this.onError = onError;
            return this;
        }

        public UnityRestRequest OnResult<T> (Action<T> onResult)
        {
            responseHandler = new SingleResponseParser<T> (onResult);
            return this;
        }

        public UnityRestRequest OnResult<T> (Action<T[]> onResult)
        {
            responseHandler = new ArrayResponseParser<T> (onResult);
            return this;
        }

        public UnityRestRequest OnResult (Action onResult)
        {
            this.onResult = onResult;
            return this;
        }

        public void Send ()
        {
            executer.StartCoroutine (SendRoutine ());
        }

        internal IEnumerator SendRoutine ()
        {
            string url = BuildURL ();

            UnityWebRequest internalRequest = BuildInternalRequest (url);

            if (headers != null)
                SetHeaders (internalRequest);
            yield return internalRequest.Send ();

            HandleResult (internalRequest);
        }

        private UnityWebRequest BuildInternalRequest (string url)
        {
            switch (verb)
            {
                case HttpVerb.Get:
                    return UnityWebRequest.Get (url);

                case HttpVerb.Post:
                    return UnityWebRequest.Post (url, body);

                default:
                    return null;
            }
        }

        private string BuildURL ()
        {
            string url = string.Format ("{0}/{1}", baseUrl, endpoint);

            if (id != null)
                url = string.Format ("{0}/{1}", url, id.value);
            return url;
        }

        private void SetHeaders (UnityWebRequest request)
        {
            foreach (string name in headers.Keys)
                request.SetRequestHeader (name, headers[name].ToString ());
        }

        private void HandleResult (UnityWebRequest request)
        {
            if (request.isError)
                TryRaiseError (request.error);
            else if (UnityRestUtils.HasErrorStatusCode (request.responseCode))
                TryRaiseError (string.Format ("Has error status code: {0}", request.responseCode));
            else if (responseHandler != null)
                responseHandler.OnResponse (request.downloadHandler.text);
            else if (onResult != null)
                onResult ();
        }

        private void TryRaiseError (string error)
        {
            if (onError != null)
                onError (error);
            else
                Debug.LogWarningFormat ("There was an error and no error handler was specified: {0}", error);
        }
    }
}