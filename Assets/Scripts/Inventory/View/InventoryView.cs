using Assets.Goo.Tools.Pooling;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private PoolingObjects _pooling;
    [SerializeField] private RectTransform _emptyMsg;

    public void Open(CharacterInventoryCollection inventory)
    {
        foreach (var item in inventory.Inventory.Values)
        {
            var view = _pooling.GetObject<InventoryItemView>();
            view.Show(item);
        }
        _emptyMsg.gameObject.SetActive(inventory.Inventory.Count == 0);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        _pooling.DisableAll();
        gameObject.SetActive(false);
    }
}
