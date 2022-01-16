using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : Ability
{
    [SerializeField] private CharacterColliderInteractions _interactions;
    [SerializeField] private float _pickableDistance = 1f;

    private const KeyCode _openInventory = KeyCode.I; // todo mock

    private CharacterInventoryVisuals _visuals;
    private CharacterInventoryCollection _inventory;

    protected void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
        _interactions.AddListener(new ColliderListener<PickableContainer>(Collect, null));
    }

    protected IEnumerator Start()
    {
        InitInteractives();
        _inventory = new CharacterInventoryCollection(Character.Id);
        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _inventory);
    }

    protected void Update()
    {
        // todo mock
        if (Input.GetKeyDown(_openInventory))
        {
            UiReferenceManager.Instance.Inventory.Open(_inventory);
        }
        if (Input.GetKeyUp(_openInventory))
        {
            UiReferenceManager.Instance.Inventory.Close();
        }
    }

    public void Collect(PickableContainer item)
    {
        _inventory.Add(item.Item);
        item.Collected();
        _visuals?.ReportCollect(item);
    }

    public Weapon GetNextWeapon()
    {
        // todo mock
        return new Weapon
        {
            Type = "Rifle"
        };
    }

    private void InitInteractives()
    {
        Character.Input.MouseInteraction.Add(new MovementMouseListener<PickableContainer>((item) =>
        {
            item.Clicked();
            Character.Movement.Tasks.Add(new MovementTask
            {
                Condition = () => Vector3.Distance(Character.Position, item.transform.position) < _pickableDistance,
                Do = () => Collect(item)
            });
        }));
    }
}
