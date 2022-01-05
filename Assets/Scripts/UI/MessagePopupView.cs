using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessagePopupView : UIWindowView
{
    [SerializeField] private TMP_Text _message;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private GameObject _blend;

    public void Open(string message)
    {
        _message.text = message;
        _blend.SetActive(true);
        gameObject.SetActive(true);
    }

    protected void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        gameObject.SetActive(false);
        _blend.SetActive(false);
    }
}

public class UIWindowView : MonoBehaviour
{
}
