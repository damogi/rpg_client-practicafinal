using Characters;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterSelection
{
    public class SelectableCharacter : MonoBehaviour, IPointerClickHandler
    {
        public CharacterData character;

        public void OnPointerClick(PointerEventData eventData)
        {
            CharacterDetailPanel.Instance.Show (character);
        }
    }
}