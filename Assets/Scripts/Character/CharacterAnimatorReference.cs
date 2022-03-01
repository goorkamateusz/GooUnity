using UnityEngine;

public class CharacterAnimatorReference : MonoBehaviour, ICharacterComponent
{
    public Character Character { get; internal set; }

    public void InjectCharacter(Character character)
    {
        Character = character;
    }
}
