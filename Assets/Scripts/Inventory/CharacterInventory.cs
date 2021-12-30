using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> _inventory = new List<InventoryItem>();
    [SerializeField] private CharacterColliderInteractions _interactions;

    private CharacterInventoryVisuals _visuals;
    private ColliderListener<InventoryItem> _inventoryItemListener = new ColliderListener<InventoryItem>();

    private void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
        _inventoryItemListener.OnTriggerEnter += Collect;
        _interactions.AddListener(_inventoryItemListener);
    }

    public void Collect(InventoryItem item)
    {
        _inventory.Add(item);
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
