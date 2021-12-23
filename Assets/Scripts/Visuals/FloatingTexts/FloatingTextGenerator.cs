using UnityEngine;

public class FloatingTextGenerator : MonoBehaviour
{
    [SerializeField] private FloatingText _prefab;

    public void ShowText(string msg)
    {
        var ob = Instantiate<FloatingText>(_prefab, transform.position, Camera.main.transform.rotation);
        ob.SetText(msg);
    }
}
