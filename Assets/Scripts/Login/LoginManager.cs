using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Login
{
	public class LoginManager : MonoBehaviour
	{
		[SerializeField]
		private int mainScene;
		[SerializeField]
		private InputField username;

		public void Login ()
		{
			ServicesFacade.Instance.Login (username.text, LoadMainScene);
		}

		private void LoadMainScene ()
		{
			SceneManager.LoadScene (mainScene);
		}
	}
}