using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DoorBase : SceneInteractiveElement
{
    [Header("Config")]
    [SerializeField] private bool _isOpen;
    [SerializeField] private bool _openAutomatically;
    [SerializeField] private bool _closeAutomatically;

    [Tooltip("If not null it disable obstacle for openned door")]
    [SerializeField] private NavMeshObstacle _obstacle;

    [Header("Tips messages")]
    [SerializeField, TextArea] private string _tipOpenMessage;
    [SerializeField, TextArea] private string _tipCloseMessage;

    protected Coroutine _coroutine;

    public bool IsOpen { get => _isOpen; private set => _isOpen = value; }
    public bool AutoOpen => _openAutomatically;
    public bool AutoClose => _openAutomatically;

    public override void ColiderEnter(IPlayerInteractiveComponent player)
    {
        if (AutoOpen)
            Open();
        else
            DisplayTip(player);
    }

    public override void ColiderExit(IPlayerInteractiveComponent player)
    {
        if (AutoClose)
            Close();
    }

    public override void OnKeyDown(IPlayerInteractiveComponent player)
    {
        Toggle();
        DisplayTip(player);
    }

    public override void OnKeyUp(IPlayerInteractiveComponent player)
    {
    }

    protected override void DisplayTip(IPlayerInteractiveComponent player)
    {
        DisplayTip(player, IsOpen ? _tipCloseMessage : _tipOpenMessage);
    }

    private void Close()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(OpenAnimation());
            IsOpen = false;
            ToggleObstacle();
        }
    }

    private void Open()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CloseAnimation());
            IsOpen = true;
            ToggleObstacle();
        }
    }

    protected virtual IEnumerator OpenAnimation()
    {
        _coroutine = null;
        yield break;
    }

    protected virtual IEnumerator CloseAnimation()
    {
        _coroutine = null;
        yield break;
    }

    private void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }

    private void ToggleObstacle()
    {
        if (_obstacle)
            _obstacle.enabled = !IsOpen;
    }

    protected void Start()
    {
        if (IsOpen)
            Open();
    }
}