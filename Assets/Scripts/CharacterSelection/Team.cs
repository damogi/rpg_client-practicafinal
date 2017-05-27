using System;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using Characters;
using Services;

namespace CharacterSelection
{
	[Serializable]
    [Description("team")]
	public class Team
	{
        public int character1;
        public int character2;
        public int character3;

        public List<CharacterData> characters = new List<CharacterData>();

        public static Team actualTeam;

		public Team ()
		{
            characters = new List<CharacterData>();
		}

		public void AddCharacter (CharacterData character)
		{
            Debug.Log("AddCharacter");
            characters.Add (character);
		}

		public void RemoveCharacter (CharacterData character)
		{
            Debug.Log("RemoveCharacter");
            characters.Remove (character);
        }

        public string Print()
        {
            AddCharacter(ServicesFacade.Instance.catalog.characters[character1-1]);
            AddCharacter(ServicesFacade.Instance.catalog.characters[character2-1]);
            AddCharacter(ServicesFacade.Instance.catalog.characters[character3-1]);

            return string.Format("{0}, {1}, {2}", characters[0].name, characters[1].name, characters[2].name);
        }
	}
}