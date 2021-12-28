using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    public void SetBool(string name, bool value = true)
    {
        _animator.SetBool(name, value);
    }

    protected virtual void Update()
    {
        _animator.SetFloat("Speed", _player.Movement.CurrentSpeed / (_player.Movement.OriginalSpeed + .001f));
    }
}