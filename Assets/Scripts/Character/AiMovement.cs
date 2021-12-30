using UnityEngine;

public class AiMovement : PlayerMovement
{
    [SerializeField] private Transform[] _path;
    [SerializeField] const float _minDistance = 2f;

    private int _nextPoint = -1;

    public Transform NextPoint => _path[_nextPoint];
    public float DistanceToNext => Vector3.Distance(Player.Position, NextPoint.position);

    protected override void HandleInput()
    {
    }

    protected override void Awake()
    {
        base.Awake();
        GoNext();
    }

    protected override void Update()
    {
        base.Update();
        Movement();
    }

    private void Movement()
    {
        if (DistanceToNext < _minDistance)
            GoNext();
    }

    private void GoNext()
    {
        _nextPoint = (_nextPoint + 1) % _path.Length;
        _agent.SetDestination(NextPoint.position);
    }
}