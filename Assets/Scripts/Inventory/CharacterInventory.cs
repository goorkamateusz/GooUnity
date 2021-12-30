using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> _inventory = new List<InventoryItem>();
    [SerializeField] private CharacterColliderInteractions _interactions;

    private CharacterInventoryVisuals _visuals;

    private void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
        _interactions.AddListener(new ColliderListener<InventoryItem>(Collect, null));
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
