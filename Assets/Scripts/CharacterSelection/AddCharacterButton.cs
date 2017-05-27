using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterSelection
{
	[RequireComponent (typeof (Button))]
	public class AddCharacterButton : MonoBehaviour 
	{
		public bool isAvailable;
		private Button button;
		public CharacterData character;

		private void Awake ()
		{
			button = GetComponent<Button> ();
			button.onClick.AddListener (OnClick);
		}

		public void Setup (CharacterData character, bool forceAvailable = false)
		{
			this.character = character;
			SetAvailable (forceAvailable || character.isAvailable);
			button.image.sprite = character.portrait;
		}

		private void SetAvailable (bool isAvailable)
		{
			this.isAvailable = isAvailable || character.isAvailable;
			button.image.color = this.isAvailable? Color.white : Color.gray;
		}

		private void OnClick () 
		{
			if (isAvailable)
				TeamManager.Instance.AddCharacter (character);
			else
				CharacterDetailPanel.Instance.Show (character);
		}
	}
}