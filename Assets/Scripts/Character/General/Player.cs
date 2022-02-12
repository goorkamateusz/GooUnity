using UnityEngine;

public class Player : Character
{
    [Header("Player")]
    [SerializeField] private CharacterInput _characterInput;

    public CharacterInput Input => _characterInput;
}
