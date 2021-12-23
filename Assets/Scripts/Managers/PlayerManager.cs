using UnityEngine;

public class PlayerManager : SceneSingleton<PlayerManager>
{
    [SerializeField] private Player _player;

    public Player Player => _player;
}
