using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : Ability
{
    [SerializeField] private CharacterColliderInteractions _interactions;
    [SerializeField] private float _pickableDistance = 1f;

    private const KeyCode _openInventory = KeyCode.I; // todo mock

    private CharacterInventoryVisuals _visuals;
    private CharacterInventoryCollection _inventory;

    protected void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
    }

    protected IEnumerator Start()
    {
        InitInteractives();
        _inventory = new CharacterInventoryCollection(Character.Id);
        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _inventory);
    }

    protected void Update()
    {
        // todo mock
        if (Input.GetKeyDown(_openInventory))
        {
            UiReferenceManager.Instance.Inventory.Open(_inventory);
        }
        if (Input.GetKeyUp(_openInventory))
        {
            UiReferenceManager.Instance.Inventory.Close();
        }
    }

    public Weapon GetNextWeapon()
    {
        // todo mock
        return new Weapon
        {
            Type = "Rifle"
        };
    }

    private void InitInteractives()
    {
        Character.ColliderInteractions?.AddListener(new ColliderListener<PickableContainer>(Collect, null));
        Character.Input?.MouseInteraction.Add(new MovementMouseListener<PickableContainer>(HandlePickable));
    }

    private void HandlePickable(PickableContainer obj)
    {
        obj.Clicked();
        Character.Movement.Tasks.Add(new MovementTask
        {
            Condition = () => Vector3.Distance(Character.Position, obj.transform.position) < _pickableDistance,
            Do = () => Collect(obj)
        });
    }

    private void Collect(PickableContainer item)
    {
        _inventory.Add(item.Item);
        item.Collected();
        _visuals?.ReportCollect(item);
    }
}
