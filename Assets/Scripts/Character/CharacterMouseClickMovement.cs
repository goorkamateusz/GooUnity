using UnityEngine;

public class CharacterMouseClickMovement : PlayerMovement
{
    [Header("Mouse")]
    [SerializeField] private int _mouseButtonNumber = 0;

    private Camera _main;

    protected override void HandleInput()
    {
        if (Input.GetMouseButtonDown(_mouseButtonNumber))
        {
            var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _main = Camera.main;
    }
}
