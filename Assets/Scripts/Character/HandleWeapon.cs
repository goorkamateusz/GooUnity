using UnityEngine;

public class HandleWeapon : InputOrientedAbility
{
    [Header("Input")]
    [SerializeField] private KeyCode _pickWeapon;
    [SerializeField] private int _mouseButtonId;

    [Header("Weapon")]
    [SerializeField] private CharacterInventory _inventory; // todo interface
    [SerializeField] private PlayerController _playerController;

    private Weapon _weapon = null;

    public bool IsNotHandled => _weapon is null;

    protected override void HandleInput()
    {
        if (Input.GetKeyDown(_pickWeapon))
        {
            PickWeapon();
        }

        // todo walka w ręcz
        if (IsNotHandled) return;

        if (Input.GetMouseButtonDown(_mouseButtonId))
        {

        }
    }

    private void PickWeapon()
    {
        if (IsNotHandled)
        {
            _weapon = _inventory?.GetNextWeapon();
            _playerController.SetArsenal(_weapon.Type);
        }
        else
        {
            _playerController.SetArsenal("Empty");
            _weapon = null;
        }
    }
}