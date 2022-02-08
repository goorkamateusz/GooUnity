using UnityEngine;

public class HandleWeapon : Ability
{
    [Header("Input")]
    [SerializeField] private KeyCode _pickWeaponKey;
    [SerializeField] private KeyCode _aimingToggleKey;
    [SerializeField] private KeyCode _attackKey;
    [SerializeField] private int _mouseButtonId;

    [Header("Weapon")]
    [SerializeField] private CharacterInventory _inventory; // todo interface

    private Weapon _weapon = null;
    private bool _aiming;

    public bool IsNotHandled => _weapon is null;

    protected virtual void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        if (Input.GetKeyDown(_pickWeaponKey))
        {
            PickWeapon();
        }

        // todo walka w ręcz
        // if (IsNotHandled) return;

        if (Input.GetKeyDown(_aimingToggleKey))
        {
            SetAiming(!_aiming);
        }

        if (Input.GetKeyDown(_attackKey))
        {
            SetAiming(true);
            Character.AnimatorHandler.SetTrigger("Attack");
        }

        if (Input.GetMouseButtonDown(_mouseButtonId))
        {

        }
    }

    private void SetAiming(bool state)
    {
        _aiming = state;
        Character.AnimatorHandler.SetBool("Aiming", _aiming);
    }

    private void PickWeapon()
    {
        // todo
    }
}