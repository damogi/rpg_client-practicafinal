using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterSelection
{
    public class CharacterDetailPanel: MonoBehaviour
    {
        [SerializeField]
        private Text nameLabel;
        [SerializeField]
        private Text lifepointsLabel;
        [SerializeField]
        private Text attackpointsLabel;
        [SerializeField]
        private Text defensepointsLabel;
        [SerializeField]
        private Image portrait;
        [SerializeField]
        private Button removeButton;
        [SerializeField]
        private Button purchaseButton;

        private CharacterData character;

        public static CharacterDetailPanel Instance
        {
            get; private set;
        }

        private void Awake ()
        {
            Instance = this;
            Hide ();
        }

        public void Show (CharacterData character)
        {
            this.character = character;
            nameLabel.text = character.name;
            lifepointsLabel.text = character.lifepoints.ToString ();
            attackpointsLabel.text = character.attackpoints.ToString ();
            defensepointsLabel.text = character.defensepoints.ToString ();
            portrait.sprite = character.portrait;
            gameObject.SetActive (true);
            SetActionButton (character);
        }

        public void Hide ()
        {
            gameObject.SetActive (false);
        }

        public void RemoveCurrentCharacter ()
        {
            TeamManager.Instance.RemoveCharacter (character);
            Hide ();
        }

        public void PurchaseCurrentCharacter ()
        {
            CharacterPurchaser.Instance.StartPurchase (character);
            Hide ();
        }

        private void SetActionButton(CharacterData character)
        {
            purchaseButton.gameObject.SetActive (!character.isAvailable);
            removeButton.gameObject.SetActive (character.isAvailable);
        }
    }
}