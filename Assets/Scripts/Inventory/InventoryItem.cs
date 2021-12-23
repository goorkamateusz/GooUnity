using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InventoryItem : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private GameObject _model;
    [SerializeField] private Collider _colider;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _onCollected;

    public void Collected()
    {
        Debug.Log("Ah... MASZ MNIE!");
        _colider.enabled = false;
        _onCollected?.Play();
        _model.SetActive(false);
    }

    private void Reset()
    {
        var colider = GetComponent<Collider>();
        colider.isTrigger = true;
    }
}
