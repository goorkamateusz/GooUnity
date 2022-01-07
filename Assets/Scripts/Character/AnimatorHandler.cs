using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    public void SetInt(string name, int value)
    {
        _animator.SetInteger(name, value);
    }

    public void SetBool(string name, bool value = true)
    {
        _animator.SetBool(name, value);
    }

    protected virtual void Update()
    {
        _animator.SetFloat("Speed", _character.Movement.CurrentSpeed / (_character.Movement.OriginalSpeed + .001f));
    }
}