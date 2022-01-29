using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class CharacterPathfinding : ICharacterComponent
{
    [SerializeField] private NavMeshAgent _agent;

    private Character _character;

    public bool Active { get; internal set; }
    public NavMeshAgent Agent => _agent;

    public void InjectCharacter(Character character)
    {
        _character = character;
    }

    internal void ResetPath()
    {
        throw new NotImplementedException();
    }

    public void SetDestination(Vector3 target)
    {
        // todo pathfinding in navmashagant but moving realise with char. controller
        _agent.Warp(_character.Position);
        _agent.SetDestination(target);
    }

    public void Update()
    {
        // todo
    }

    internal void Disable()
    {
        _agent.updatePosition = _agent.updateRotation = false;
    }

    internal void Enable()
    {
        _agent.updatePosition = _agent.updateRotation = true;
    }
}
