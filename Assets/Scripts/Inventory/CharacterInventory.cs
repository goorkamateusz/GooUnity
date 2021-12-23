using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> _inventory = new List<InventoryItem>();

    private void Awake()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.gameObject.GetComponent<InventoryItem>();
        if (item)
            Collect(item);
    }

    private void Collect(InventoryItem item)
    {
        _inventory.Add(item);
        item.Collected();
    }
}
