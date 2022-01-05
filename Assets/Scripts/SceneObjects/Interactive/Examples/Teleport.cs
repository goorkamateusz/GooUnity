using UnityEngine;

public class Teleport : SceneInteractiveElement
{
    [SerializeField] private Teleport _target;
    [SerializeField] private bool _acceptAi;

    private bool _disabled = false;

    public override void ColiderEnter(IPlayerInteractiveComponent player)
    {
        if (_disabled) return;

        if (player.IsPlayer || _acceptAi)
        {
            _target.Move(player);
        }
    }

    public override void ColiderExit(IPlayerInteractiveComponent player)
    {
        _disabled = false;
    }

    public override void OnKeyDown(IPlayerInteractiveComponent player)
    {
        throw new System.NotImplementedException();
    }

    public override void OnKeyUp(IPlayerInteractiveComponent player)
    {
        throw new System.NotImplementedException();
    }

    public void Move(IPlayerInteractiveComponent character)
    {
        character.Character.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _disabled = true;
        // idea visuals
    }
}