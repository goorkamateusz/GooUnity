using System.Collections;
using UnityEngine;
using Goo.Saves;
using Assets.Goo.Characters.Interactions;
using Assets.Goo.Characters.Ability;

public class CharacterInventory : Ability
{
    [SerializeField] private float _pickableDistance = 1f;
    [SerializeField] private KeyCode _openInventory = KeyCode.I;

    private CharacterInventoryVisuals _visuals;
    private CharacterInventoryCollection _inventory;

    protected void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
    }

    protected IEnumerator Start()
    {
        Character.Input.KeyInteractions.Add(new PersistantKeyHandler
        {
            Key = _openInventory,
            OnKeyDown = OnKeyDown,
            OnKeyUp = OnKeyUp
        });
        InitInteractives();
        _inventory = new CharacterInventoryCollection(Character.Id);
        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _inventory);
    }

    private void OnKeyUp()
    {
        UiReferenceManager.Instance.Inventory.Close();
    }

    private void OnKeyDown()
    {
        UiReferenceManager.Instance.Inventory.Open(_inventory);
    }

    private void InitInteractives()
    {
        Character.ColliderInteractions?.Add(new ColliderListener<PickableContainer>(Collect, null));
        Character.Input?.MouseInteraction.Add(new MovementMouseListener<PickableContainer>(HandlePickable));
    }

    private void HandlePickable(PickableContainer obj)
    {
        obj.Clicked();
    }

    private void Collect(PickableContainer item)
    {
        _inventory.Add(item.Item);
        item.Collected();
        _visuals?.ReportCollect(item);
    }
}
