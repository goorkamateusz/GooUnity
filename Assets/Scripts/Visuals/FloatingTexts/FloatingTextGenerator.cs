using UnityEngine;
using Goo.Tools.Pooling;

public class FloatingTextGenerator : MonoBehaviour
{
    [SerializeField] private SimpleObjectPooler _pooling;

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
