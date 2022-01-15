using UnityEngine;

// idea container & pooling in seperated component for dynamic UI
public class InventoryView : MonoBehaviour
{
    [SerializeField] private PoolingObjects _pooling;

    public void Open(CharacterInventoryCollection inventory)
    {
        foreach (var item in inventory.Inventory.Values)
        {
            var view = _pooling.GetObject<InventoryItemView>();
            view.Show(item);
        }
        gameObject.SetActive(true);
    }

    public void Close()
    {
        _pooling.DisableAll();
        gameObject.SetActive(false);
    }
}
