using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;

    public void Show(CharacterInventoryCollection.Data inv)
    {
        _icon.sprite = inv.Item.Icon;
        _name.text = $"{inv.Item.name} x{inv.Number}";
    }
}
