using UnityEngine;

public class CharacterMouseClickMovement : CharacterMovement
{
    [Header("Mouse")]
    [SerializeField] private ParticleSystem _mouseClickEffect;

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
            _agent.SetDestination(hit.point);
            _mouseClickEffect.transform.position = hit.point;
            _mouseClickEffect.Play();
        }
    }

    private void FollowOtherCharacter()
    {
        Character.Input.MouseInteraction.Add(new MovementMouseListener<Character>((other) =>
        {
            Tasks.Add(new MovementTask
            {
                Condition = () => Vector3.Distance(Character.Position, other.Position) < 4f,
                Do = () => Stop(),
                Otherwise = () => _agent.SetDestination(other.Position),
                DisableAutoDelete = true
            });
        }));
    }
}
