using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class DegreesText : MonoBehaviour
{
    [SerializeField] TMP_Text _txt;

    [ContextMenu("UpdateText")]
    protected void UpdateText()
    {
        SetText();
    }

    private void SetText()
    {
        if (_txt)
            _txt.text = GetAxis().ToString("N0") + "°";
    }

    private float GetAxis()
    {
        if (transform.eulerAngles.x > 180)
            return 360 - transform.eulerAngles.x;
        return transform.eulerAngles.x;
    }
}
