using System.Collections.Generic;
using UnityEngine;

public struct CharacterInputAction
{
    public delegate void OnKeyDelegate();
    public delegate void OnKeyUpDelegate();

    public KeyCode Key;
    public OnKeyDelegate OnKeyDown;
    public OnKeyUpDelegate OnKeyUp;

    public bool ProcessAction()
    {
        if (Input.GetKeyDown(Key))
        {
            if (OnKeyDown != null)
                OnKeyDown();
        }
        if (Input.GetKeyUp(Key))
        {
            if (OnKeyUp != null)
                OnKeyUp();
            return true;
        }
        return false;
    }
}

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private int _mouseButtonNumber = 0;

    private List<CharacterInputAction> _actions = new List<CharacterInputAction>();
    private MovementMouseInteractions _listener = new MovementMouseInteractions();
    private Camera _main;
    private RaycastHit _hit;

    public MovementMouseInteractions MouseInteraction => _listener;
    public RaycastHit Hit => _hit;
    public bool Clicked { get; private set; }

    public void AddAction(CharacterInputAction action)
    {
        _actions.Add(action);
    }

    public void RemoveAction(CharacterInputAction action)
    {
        _actions.Remove(action);
    }

    protected void Awake()
    {
        _main = Camera.main;
    }

    protected void Update()
    {
        for (int i = _actions.Count - 1; i > -1; i--)
        {
            if (_actions[i].ProcessAction())
                _actions.RemoveAt(i);
        }

        Clicked = false;

        if (Input.GetMouseButtonDown(_mouseButtonNumber))
        {
            var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
            {
                _hit = hit;
                Clicked = true;
            }
        }
    }

    protected void LateUpdate()
    {
        if (Clicked)
        {
            _listener.CheckAll(_hit);
        }
    }
}