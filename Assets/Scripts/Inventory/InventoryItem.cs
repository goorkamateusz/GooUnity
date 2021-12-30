using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InventoryItem : MonoBehaviour
{
    [Tooltip("It changing IsTrigger field on colider")]
    [SerializeField] private bool _autoPickUp;

    [Header("Model")]
    [SerializeField] private GameObject _model;
    [SerializeField] private Collider _colider;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _onCollected;
    [SerializeField] private FloatingTextGenerator _floatingText;

    public void Collected()
    {
        Debug.Log("Ah... MASZ MNIE!");
        _colider.enabled = false;
        _onCollected?.Play();
        _model.SetActive(false);
    }

    public void Clicked()
    {
        _floatingText.ShowText("Klikłeś to zbierz mnie");
    }

    protected void Awake()
    {
        _colider.isTrigger = _autoPickUp;
    }

    private void Reset()
    {
        var colider = GetComponent<Collider>();
        colider.isTrigger = true;
    }
}
