using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : Ability
{
    [SerializeField] private CharacterColliderInteractions _interactions;
    private const KeyCode _openInventory = KeyCode.I;

    private CharacterInventoryVisuals _visuals;
    private CharacterInventoryCollection _inventory;

    protected void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
        _interactions.AddListener(new ColliderListener<PickableContainer>(Collect, null));
    }

    protected IEnumerator Start()
    {
        _inventory = new CharacterInventoryCollection(Character.Id);
        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _inventory);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(_openInventory))
        {
            UiReferenceManager.Instance.Inventory.Open(_inventory);
        }
        if (Input.GetKeyUp(_openInventory))
        {
            UiReferenceManager.Instance.Inventory.Close();
        }
    }

    public void Collect(PickableContainer item)
    {
        _inventory.Add(item.Item);
        item.Collected();
        _visuals?.ReportCollect(item);
    }

    public Weapon GetNextWeapon()
    {
        // todo mock
        return new Weapon
        {
            Type = "Rifle"
        };
    }
}
