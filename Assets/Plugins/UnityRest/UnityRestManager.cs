using System.Collections;
using UnityEngine;

namespace UnityRest
{
	public class UnityRestManager : MonoBehaviour, ICoroutineExecuter
	{
		[SerializeField]
		private string serverUrl;
		[SerializeField]
		private int port = 80;

		public static UnityRestManager Instance
		{
			get; private set;
		}

		public string Url
		{
			get
			{
				if (port != 0 && port != 80)
					return string.Format ("{0}:{1}", serverUrl, port);
				else
					return serverUrl;
			}
		}

		private void Awake ()
		{
			if (Instance != null)
				Destroy (this);
			else
			{
				Instance = this;
				DontDestroyOnLoad (gameObject);
			}
		}

		public UnityRestRequest Get<T> (string endpoint = null)
		{
			if (string.IsNullOrEmpty(endpoint))
				endpoint = UnityRestUtils.GetDescription (typeof (T));
			return new UnityRestRequest (HttpVerb.Get, Url, endpoint, this);
		}

		public UnityRestRequest Post<T> (string endpoint = null)
		{
			if (string.IsNullOrEmpty(endpoint))
				endpoint = UnityRestUtils.GetDescription (typeof (T));
			return new UnityRestRequest (HttpVerb.Post, Url, endpoint, this);
		}

        void ICoroutineExecuter.StartCoroutine(IEnumerator coroutine)
        {
            StartCoroutine (coroutine);
        }
    }
}