using System.Collections.Generic;
using Characters;
using Players;
using Services;
using UnityEngine;
using UnityEngine.UI;
using UnityRest;

namespace CharacterSelection
{
    public class CharacterPurchaser: MonoBehaviour
    {
        [SerializeField]
        private Text costLabel;
        [SerializeField]
        private Text nameLabelPurchase;
        private CharacterData character;

        public static CharacterPurchaser Instance
        {
            get; private set;
        }

        private void Awake ()
        {
            Instance = this;
            Hide ();
        }

        public void StartPurchase (CharacterData character)
        {
            this.character = character;
            nameLabelPurchase.text=character.name;
            gameObject.SetActive (true);
        }

        public void Confirm ()
        {
            if (Player.Instance.Gold >= character.cost)
                ServicesFacade.Instance.PurchaseCharacter (character, OnPurchaseCompleted);
            Hide ();
        }

        public void Hide ()
        {
            gameObject.SetActive (false);
        }

        private void OnPurchaseCompleted ()
        {
            Player.Instance.UseGold (character.cost);
            AvailableCharactersPanel.Instance.SetAvailableCharacters (new List<ObjectId> () {character.id});
        }
    }
}