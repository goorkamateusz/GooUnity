using UnityEngine;
using Assets.Goo.Tools.PoolingObjects;

public class FloatingTextGenerator : MonoBehaviour
{
    [SerializeField] private PoolingObjects _pooling;

    public void ShowText(string msg)
    {
        Transform cameraTransform = Camera.main.transform;
        var ob = _pooling.GetObject(transform.position, cameraTransform.rotation).GetComponent<FloatingText>();
        ob.Launch(msg);
    }

    [ContextMenu("Test")]
    private void Test()
    {
        ShowText("Test");
    }
}
