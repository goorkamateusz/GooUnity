using UnityEngine;

namespace Assets.Goo.Characters.General
{
    public class CharacterReferenceComponent : MonoBehaviour, ICharacterComponent
    {
        public Character Character { get; internal set; }

        public void InjectCharacter(Character character)
        {
            Character = character;
        }
    }
}