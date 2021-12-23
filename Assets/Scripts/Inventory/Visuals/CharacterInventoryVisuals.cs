using UnityEngine;

public class CharacterInventoryVisuals : MonoBehaviour
{
    [SerializeField] private FloatingTextGenerator _floatingText;

    public void ReportCollect(InventoryItem item)
    {
        _floatingText.ShowText("Jej! Mam prezent!");
    }
}
