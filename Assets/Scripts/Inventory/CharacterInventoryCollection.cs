using System.Collections.Generic;
using Newtonsoft.Json;

public class CharacterInventoryCollection : AttributedSaveSerializable
{
    public class Data
    {
        public int Number;

        private InventoryItem _item;

        [JsonIgnore] public string Id { get; private set; }
        [JsonIgnore] public InventoryItem Item => GetItem();

        public Data() { }

        public Data(InventoryItem item)
        {
            Number = 0;
            _item = item;
        }

        private InventoryItem GetItem()
        {
            if (_item == null && ItemsRepo.Initialized)
                _item = ItemsRepo.Instance[Id];
            return _item;
        }

        internal void Inject(KeyValuePair<string, Data> pair)
        {
            Id = pair.Key;
        }
    }

    public Dictionary<string, Data> Inventory = new Dictionary<string, Data>();

    public override string SubKey => "inventory";
    public CharacterInventoryCollection(string parentKey) : base(parentKey) { }

    protected override void AfterLoad()
    {
        foreach (var pair in Inventory)
            pair.Value.Inject(pair);
    }

    public void Add(InventoryItem item)
    {
        if (!Inventory.ContainsKey(item.Id))
            Inventory[item.Id] = new Data(item);
        Inventory[item.Id].Number++;
    }
}
