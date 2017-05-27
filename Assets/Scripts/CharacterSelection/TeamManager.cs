using System.Collections.Generic;
using UnityEngine;
using UnityRest;
using Characters;
using Services;
using Utils;

namespace CharacterSelection
{
	public class TeamManager : MonoBehaviour
	{
        public Team team;
        [SerializeField]
		private int size;
		[SerializeField]
		private GameObjectHorizontalLayout layout;

		public static TeamManager Instance
		{
			get; private set;
		}

		private void Awake ()
		{
			Instance = this;
		}

		private void Start () 
		{
			ServicesFacade.Instance.FetchTeam (SetCurrentTeam);
		}

		public void AddCharacter (CharacterData character)
		{
            if (team == null)
                team = Team.actualTeam;

            CheckForCharacter(character);
			AddToLayout (character);
        }

        private void CheckForCharacter(CharacterData character)
        {
            int characterIndex = 0;

            switch (Team.actualTeam.characters.Count)
            {
                case 0:
                    Team.actualTeam.characters.Add(character);
                    int.TryParse(Team.actualTeam.characters[0].id.value, out characterIndex);
                    Team.actualTeam.character1 = characterIndex;
                    break;

                case 1:
                    Team.actualTeam.characters.Add(character);
                    int.TryParse(Team.actualTeam.characters[1].id.value, out characterIndex);
                    Team.actualTeam.character2 = characterIndex;
                    break;

                case 2:
                    Team.actualTeam.characters.Add(character);
                    int.TryParse(Team.actualTeam.characters[2].id.value, out characterIndex);
                    Team.actualTeam.character3 = characterIndex;
                    break;

                default:
                    Team.actualTeam.characters.Clear();
                    Team.actualTeam.characters.Add(character);
                    int.TryParse(Team.actualTeam.characters[0].id.value, out characterIndex);
                    Team.actualTeam.character1 = characterIndex;
                    break;
            }
        }

        public void RemoveCharacter (CharacterData character)
		{
			if (team == null || team.characters.Count == 0) return;
			team.RemoveCharacter (character);
			layout.Remove ((gameObject) => gameObject.GetComponent<SelectableCharacter> ().character.id == character.id);
		}

		public void SaveTeam ()
		{
			if (team == null) return;
			ServicesFacade.Instance.SaveTeam (() => Debug.Log ("Saved"));
		}

		private void SetCurrentTeam (Team team)
		{
			this.team = team;

            foreach (CharacterData character in team.characters)
				AddToLayout (character);
		}

		private void AddToLayout(CharacterData character)
        {
            SelectableCharacter selectable = Instantiate<SelectableCharacter> (character.prefab, transform);
			selectable.character = character;
			layout.Add (selectable.gameObject);
        }
	}
}