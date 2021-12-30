using UnityEngine;

public class InventoryBouncing : MonoBehaviour
{
    [SerializeField] private Vector3 _amplitude;
    [SerializeField] private float _duration;

    [SerializeField] private Transform _archer;
    [SerializeField] private Transform _model;

    private float _timer;

    private void Start()
    {
        _timer = Random.Range(-_duration, _duration);
    }

    private void Update()
    {
        if (_timer < _duration)
        {
            _timer += Time.deltaTime;
            float r = Mathf.Abs(_timer) / _duration;
            _model.position = Vector3.Lerp(_archer.position, _archer.position + _amplitude, r);
        }
        else
        {
            _timer = -_duration;
        }
    }
}
