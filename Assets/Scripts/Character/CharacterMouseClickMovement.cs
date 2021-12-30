using System;
using System.Collections.Generic;
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
        ProccessInventory(hit);

        _agent.SetDestination(hit.point);
        _mouseClickEffect.transform.position = hit.point;
        _mouseClickEffect.Play();
    }

    private void ProccessInventory(RaycastHit hit)
    {
        if (_inventory)
        {
            var item = hit.GetComponent<InventoryItem>();
            if (item)
            {
                item.Clicked();
                _tasks.Add(new MovementTask
                {
                    Condition = () => Vector3.Distance(Player.Position, item.transform.position) < _pickableDistance,
                    Do = () => _inventory.Collect(item)
                });
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _main = Camera.main;
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
