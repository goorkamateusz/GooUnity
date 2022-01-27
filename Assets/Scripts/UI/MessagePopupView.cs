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
        TimeManager.Instance.StopTime();
    }

    public void Open(string message, ICharacterInteractiveComponent character)
    {
        Open(message);
        character.Character.Input.KeyInteractions.Add(new SingleUseKeyHandler
        {
            Key = KeyCode.Escape,
            OnKeyUp = Close
        });
    }

    protected void Awake()
    {
        _button.onClick.AddListener(Close);
    }

    private void Close()
    {
        gameObject.SetActive(false);
        _blend.SetActive(false);
        TimeManager.Instance.ResetTime();
    }
}

public class UIWindowView : MonoBehaviour
{
}
