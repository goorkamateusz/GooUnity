using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterInventory : Ability
{
    private class CharacterInventoryData : AttributedSaveSerializable
    {
        public override string SubKey => "inventory";
        public CharacterInventoryData(string parentKey) : base(parentKey) { }

        public void Add(InventoryItem item)
        {
            // todo
        }
    }

    [SerializeField] private CharacterColliderInteractions _interactions;

    private CharacterInventoryVisuals _visuals;
    private CharacterInventoryData _inventory;

    protected void Awake()
    {
        _visuals = GetComponent<CharacterInventoryVisuals>();
        _interactions.AddListener(new ColliderListener<PickableContainer>(Collect, null));
    }

    protected IEnumerator Start()
    {
        _inventory = new CharacterInventoryData(Character.Id);
        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _inventory);
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
}
