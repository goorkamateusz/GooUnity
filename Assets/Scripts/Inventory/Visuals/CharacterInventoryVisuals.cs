using UnityEngine;

public class CharacterInventoryVisuals : MonoBehaviour
{
    [SerializeField] private FloatingTextGenerator _floatingText;

    public void ReportCollect(PickableContainer item)
    {
        // _floatingText.ShowText("Jej! Mam prezent!");
        _floatingText.ShowText(item.Item.Name);
    }
}
