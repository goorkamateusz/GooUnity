using UnityEngine;

public class CharacterMouseClickMovement : CharacterMovement
{
    [Header("Mouse")]
    [SerializeField] private ParticleSystem _mouseClickEffect;

    public override float CurrentSpeed => _pathfinding.Agent.velocity.magnitude; // todo

    public override void Stop()
    {
        _pathfinding.ResetPath();
    }

    public override void Wrap(Vector3 position, Quaternion rotation)
    {
        _pathfinding.Agent.Warp(position);
        Character.transform.rotation = rotation;
    }

    protected override void AfterGameLoaded()
    {
        FollowOtherCharacter();
    }

    protected override void HandleInput()
    {
        if (Character.Input.Clicked)
        {
            var hit = Character.Input.Hit;
            Tasks.Clear();
            _pathfinding.SetDestination(hit.point);
            _mouseClickEffect.transform.position = hit.point;
            _mouseClickEffect.Play();
        }
    }

    private void FollowOtherCharacter()
    {
        // todo separate to other class
        Character.Input.MouseInteraction.Add(new MovementMouseListener<Character>((other) =>
        {
            Tasks.Add(new MovementTask
            {
                Condition = () => Vector3.Distance(Character.Position, other.Position) < 4f,
                Do = () => Stop(),
                Otherwise = () => _pathfinding.SetDestination(other.Position),
                DisableAutoDelete = true
            });
        }));
    }

    private void UpdateAgent()
    {
        _pathfinding.Agent.speed = Speed; // todo agent coupling
    }

    protected override void OnSpeedChange()
    {
        // todo very awful method & Agent coupling
        _pathfinding.Agent.speed = Speed;
    }
}
