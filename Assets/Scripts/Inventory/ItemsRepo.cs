using System.Linq;
using UnityEngine;

public class ItemsRepo : SceneSingleton<ItemsRepo>
{
    [SerializeField] private InventoryItem[] _items;

    public InventoryItem this[string id] => _items.FirstOrDefault((item) => item.Id == id);
}
