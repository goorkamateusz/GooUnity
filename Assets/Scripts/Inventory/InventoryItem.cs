using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "ScriptableObjects")]
public class InventoryItem : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;

    public string Id => _id;
    public string Name => _name;
}
