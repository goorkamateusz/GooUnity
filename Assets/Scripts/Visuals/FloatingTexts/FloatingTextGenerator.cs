using Goo.Tools;
using UnityEngine;

public class FloatingTextGenerator : MonoBehaviour
{
    [SerializeField] private PoolingObjects _prefab;

    public void ShowText(string msg)
    {
        Transform cameraTransform = Camera.main.transform;
        var ob = _prefab.GetObject(transform.position, cameraTransform.rotation).GetComponent<FloatingText>();
        ob.SetText(msg);
    }
}
