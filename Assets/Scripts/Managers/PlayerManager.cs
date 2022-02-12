using Assets.Goo.Tools.Patterns;
using UnityEngine;

public class PlayerManager : SceneSingleton<PlayerManager>
{
    [SerializeField] private Character _player;

    public Character Player => _player;
}
