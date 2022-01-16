using UnityEngine;

public class CharacterMouseClickMovement : CharacterMovement
{
    [Header("Mouse")]
    [SerializeField] private ParticleSystem _mouseClickEffect;

    [Header("Integrations")]
    [SerializeField] private CharacterInventory _inventory;
    [SerializeField] private float _pickableDistance = 1f;

    private MovementTaskProvider _tasks = new MovementTaskProvider();

    protected override void AfterGameLoaded()
    {
        ListenAttack();
        ListenInventory();
    }

    protected override void HandleInput()
    {
        if (Character.Input.Clicked)
        {
            var hit = Character.Input.Hit;
            _tasks.Clear();
            _agent.SetDestination(hit.point);
            _mouseClickEffect.transform.position = hit.point;
            _mouseClickEffect.Play();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (_tasks.CheckAll())
            Stop();
    }

    private void ListenAttack()
    {
        Character.Input.MouseInteraction.Add(new MovementMouseListener<Character>((other) =>
        {
            _tasks.Add(new MovementTask
            {
                Condition = () => Vector3.Distance(Character.Position, other.Position) < 4f,
                Do = () => Stop(),
                Otherwise = () => _agent.SetDestination(other.Position),
                DisableAutoDelete = true
            });
        }));
    }

    private void ListenInventory()
    {
        if (_inventory)
        {
            Character.Input.MouseInteraction.Add(new MovementMouseListener<PickableContainer>((item) =>
            {
                item.Clicked();
                _tasks.Add(new MovementTask
                {
                    Condition = () => Vector3.Distance(Character.Position, item.transform.position) < _pickableDistance,
                    Do = () => _inventory.Collect(item)
                });
            }));
        }
    }
}
