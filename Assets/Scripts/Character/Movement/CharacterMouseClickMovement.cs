using System.Runtime.CompilerServices;
using System;
using UnityEngine;

public class CharacterMouseClickMovement : PlayerMovement
{
    [Header("Mouse")]
    [SerializeField] private int _mouseButtonNumber = 0;
    [SerializeField] private ParticleSystem _mouseClickEffect;

    [Header("Integrations")]
    [SerializeField] private CharacterInventory _inventory;
    [SerializeField] private float _pickableDistance = 1f;

    private Camera _main;
    private MovementTaskProvider _tasks = new MovementTaskProvider();
    private MovementMouseInteractions _listener = new MovementMouseInteractions();

    protected override void HandleInput()
    {
        if (Input.GetMouseButtonDown(_mouseButtonNumber))
        {
            var mouseRay = _main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hit, Mathf.Infinity))
            {
                ProcessHit(hit);
            }
        }
    }

    private void ProcessHit(RaycastHit hit)
    {
        _tasks.Clear();
        _listener.CheckAll(hit);

        _agent.SetDestination(hit.point);
        _mouseClickEffect.transform.position = hit.point;
        _mouseClickEffect.Play();
    }

    private void ListenAttack()
    {
        if (true)
        {
            var action = new MovementMouseListener<Player>();
            action.Action += (other) =>
            {
                _tasks.Add(new MovementTask
                {
                    Condition = () => Vector3.Distance(Player.Position, other.Position) < 4f,
                    Do = () => Stop(),
                    Otherwise = () => _agent.SetDestination(other.Position),
                    DisableAutoDelete = true
                });
            };
            _listener.Add(action);
        }
    }

    private void ListenInventory()
    {
        if (_inventory)
        {
            var action = new MovementMouseListener<InventoryItem>();
            action.Action += (item) =>
            {
                item.Clicked();
                _tasks.Add(new MovementTask
                {
                    Condition = () => Vector3.Distance(Player.Position, item.transform.position) < _pickableDistance,
                    Do = () => _inventory.Collect(item)
                });
            };
            _listener.Add(action);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _main = Camera.main;
        ListenAttack();
        ListenInventory();
    }

    protected override void Update()
    {
        base.Update();
        if (_tasks.CheckAll())
        {
            Stop();
        }
    }
}
