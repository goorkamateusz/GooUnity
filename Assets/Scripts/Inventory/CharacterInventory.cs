using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> _inventory = new List<InventoryItem>();

    private CharacterInventoryVisuals _visuals;

    private void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hej");
        var item = other.gameObject.GetComponent<InventoryItem>();
        if (item)
            Collect(item);
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
