using UnityEngine;

public class PickableContainer : MonoBehaviour
{
    [SerializeField] private InventoryItem _item;

    [Header("Settings")]
    [Tooltip("It changing IsTrigger field on colider")]
    [SerializeField] private bool _autoPickUp;

    [Header("Model")]
    [SerializeField] private GameObject _model;
    [SerializeField] private Collider _colider;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _onCollected;
    [SerializeField] private FloatingTextGenerator _floatingText;

    public InventoryItem Item => _item;

    public virtual void Collected()
    {
        _colider.enabled = false;
        _onCollected?.Play();
        _model.SetActive(false);
    }

    public virtual void Clicked()
    {
        _floatingText.ShowText("Klikłeś to zbierz mnie");
    }

    protected virtual void Awake()
    {
        _colider.isTrigger = _autoPickUp;
    }

    protected virtual void Reset()
    {
        var colider = GetComponent<Collider>();
        colider.isTrigger = true;
    }
}
