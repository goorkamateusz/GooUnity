using Goo.Tools;
using UnityEngine;

public class PlayerManager : SceneSingleton<PlayerManager>
{
    [SerializeField] private Character _player;

    public Character Player => _player;
}
