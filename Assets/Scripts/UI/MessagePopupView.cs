using UnityEngine;
using UnityEngine.UI;
using Assets.Goo.Characters;
using Assets.Goo.SceneObjects;
using Assets.Goo.Tools.EventSystem;
using Assets.Goo.Tools.UnityHelpers;
using TMPro;

public class MessagePopupView : UIWindowView, IEventListener<MessageBox.EventMessageBox>
{
    [SerializeField] private TMP_Text _message;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private GameObject _blend;

    public void OnEvent(MessageBox.EventMessageBox e)
    {
        Open(e.Message, e.Interaction);
    }

    public void OnTrigger(string e)
    {
        throw new System.NotImplementedException();
    }

    public void Open(string message)
    {
        _message.text = message;
        _blend.Null()?.SetActive(true);
        gameObject.SetActive(true);
        TimeManager.Instance.StopTime();
    }

    public void Open(string message, ICharacterInteraction character)
    {
        Open(message);
        (character.Character as Player)?.Input.KeyInteractions.Add(new SingleUseKeyHandler
        {
            Key = KeyCode.Escape,
            OnKeyUp = Close
        });
    }

    protected void Awake()
    {
        _button.onClick.AddListener(Close);
        this.SubscribeEvent();
        gameObject.SetActive(false);
    }

    protected void OnDestroy()
    {
        this.UnsubscribeEvent();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        _blend.Null()?.SetActive(false);
        TimeManager.Instance.ResetTime();
    }
}

public class UIWindowView : MonoBehaviour
{
}
