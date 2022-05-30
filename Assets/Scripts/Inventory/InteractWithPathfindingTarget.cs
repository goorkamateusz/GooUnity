using Goo.Characters;
using Goo.Characters.Interactions;
using UnityEngine;

public class InteractWithPathfindingTarget : PlayerComponent
{
    [SerializeField] private float _minimalDistance = 1f;

    protected override void OnStart()
    {
        Character.Input?.MouseInteraction.Add(new MovementMouseListener<PathfindingTarget>(Follow));
    }

    protected virtual void Follow(PathfindingTarget target)
    {
        Character.Movement.SetDestination(target.transform);
        Character.Movement.Tasks.Add(new MovementTask
        {
            Condition = () => Vector3.Distance(Character.Position, target.transform.position) < _minimalDistance
        });
    }
}
