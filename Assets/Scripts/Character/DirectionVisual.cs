using UnityEngine;

public abstract class DirectionVisual : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;

    protected abstract Quaternion? GetRotation();

    protected virtual void Update()
    {
        Quaternion? quaternion = GetRotation();
        if (quaternion.HasValue)
        {
            transform.rotation = quaternion.Value;
            _arrow.SetActive(true);
        }
        else
        {
            _arrow.SetActive(false);
        }
    }
}
