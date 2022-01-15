using UnityEngine;

// idea container & pooling in seperated component for dynamic UI
public class InventoryView : MonoBehaviour
{
    [SerializeField] private PoolingObjects _pooling;
    [SerializeField] private Transform _container;
}
