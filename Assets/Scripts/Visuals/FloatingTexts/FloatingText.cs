using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Vector3 _move;
    [SerializeField] private float _durration;

    private float _timer = 0f;
    private Vector3 _originalPosition;

    public virtual void Launch(string msg)
    {
        _timer = 0f;
        _text.text = msg;
        _originalPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (_timer < _durration)
        {
            _timer += Time.deltaTime;
            transform.position = Vector3.Lerp(_originalPosition, _originalPosition + _move, _timer / _durration);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
