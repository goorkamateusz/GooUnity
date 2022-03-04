using System;
using Assets.Goo.Characters;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class CharacterPathfinding : ICharacterComponent
{
    [SerializeField] private NavMeshAgent _agent;

    private Character _character;
    private CharacterMovement _movement;
    private NavMeshPath _path;
    private int _i;

    public bool Active { get; internal set; }
    public NavMeshAgent Agent => _agent;

    public void InjectCharacter(Character character)
    {
        _character = character;
        _movement = _character.Movement;
        _path = new NavMeshPath();
    }

    public void ResetPath()
    {
        _path.ClearCorners();
    }

    public void SetDestination(Vector3 target)
    {
        // bug when set destination durring hold A or D
        _agent.Warp(_character.Position);
        _agent.CalculatePath(target, _path);
        _i = 0;
        Active = true;
    }

    public void Update()
    {
        // bug not working for dynamic obstacles
        if (_path.corners.Length > 0)
        {
            Vector3 currentPosition = _character.Position;
            Vector3 nextCorner = _path.corners[_i];
            float speed = _movement.Speed;
            float distance = speed * Time.deltaTime;

            Vector3 toNextCorner = nextCorner - currentPosition;
            float toNextCornerDistance = toNextCorner.magnitude;

            Vector3 move = Vector3.zero;

            while (distance > toNextCornerDistance)
            {
                move += toNextCorner;
                distance -= toNextCornerDistance;
                _i++;

                if (_i >= _path.corners.Length)
                {
                    _i = 0;
                    _movement.Move(move);
                    Disable();
                    return;
                }

                nextCorner = _path.corners[_i];
                toNextCornerDistance = nextCorner.magnitude;
            }

            move += toNextCorner.normalized * distance;

            _movement.Move(move);
        }

        for (int i = 1; i < _path.corners.Length; i++)
        {
            Debug.DrawLine(_path.corners[i-1], _path.corners[i], Color.red);
        }
    }

    internal void Disable()
    {
        _agent.updatePosition = _agent.updateRotation = false;
        Active = false;
    }

    internal void Enable()
    {
        _agent.updatePosition = _agent.updateRotation = true;
    }
}
