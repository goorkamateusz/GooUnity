using UnityEngine;
using Goo.Characters.Interactions;

public class CharacterInput : MonoBehaviour
{
    // fixme it's... not as nice as can be...
    [SerializeField] private int _mouseButtonNumber = 0;

    private KeyInteractions _keys = new KeyInteractions();
    private MouseInteractions _mouse = new MouseInteractions();
    private Camera _main;
    private RaycastHit _hit;

    public MouseInteractions MouseInteraction => _mouse;
    public KeyInteractions KeyInteractions => _keys;

    public RaycastHit Hit => _hit;
    public bool Clicked { get; private set; }

    protected void Awake()
    {
        _main = Camera.main;
    }

    protected void Update()
    {
        Clicked = false;
        _keys.CheckAll();

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
            _mouse.CheckAll(_hit);
        }
    }
}