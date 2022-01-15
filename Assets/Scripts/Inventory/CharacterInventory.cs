using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : Ability
{
    [SerializeField] private CharacterColliderInteractions _interactions;

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
