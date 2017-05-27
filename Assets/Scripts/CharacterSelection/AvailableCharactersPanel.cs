using System.Collections.Generic;
using Characters;
using Services;
using UnityEngine;
using UnityRest;

namespace CharacterSelection
{
    public class AvailableCharactersPanel: MonoBehaviour
    {
        [SerializeField]
        private AddCharacterButton prefab;
        [SerializeField]
        private Transform panel;
        [SerializeField]
        private bool forceAvailable;
        [SerializeField]
        private CharacterCatalog catalog;
        private Dictionary<string, AddCharacterButton> buttons;

        public static AvailableCharactersPanel Instance
        {
            get; private set;
        }

        private void Awake ()
        {
            Instance = this;
            buttons = new Dictionary<string, AddCharacterButton> (catalog.characters.Length);
        }

        private void Start ()
        {
            foreach (CharacterData character in catalog.characters)
                SetupNewButton (character);
            if (!forceAvailable)
                ServicesFacade.Instance.GetAvailableCharacters (SetAvailableCharacters);
        }

        public void SetAvailableCharacters (List<ObjectId> availableIds)
        {
            foreach (ObjectId id in availableIds)
            {
                int value = 0;
                int.TryParse(id.value, out value);
                string index = value.ToString();
                
                CharacterData character = buttons[index].character;
                character.isAvailable = true;
                buttons[index].Setup (character);
            }
        }

        private void SetupNewButton (CharacterData character)
        {
            AddCharacterButton button = Instantiate (prefab, panel);
            button.Setup (character, forceAvailable);
            buttons.Add (character.id, button);
        }
    }
}