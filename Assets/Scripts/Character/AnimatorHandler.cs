using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    protected virtual void Update()
    {
        _animator.SetFloat("Speed", _player.Movement.CurrentSpeed / (_player.Movement.OriginalSpeed + .001f));
    }
}