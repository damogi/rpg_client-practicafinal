using UnityEngine;

namespace Characters
{
	[CreateAssetMenu]
	public class CharacterCatalog : ScriptableObject 
	{
		public CharacterData[] characters;
	}
}