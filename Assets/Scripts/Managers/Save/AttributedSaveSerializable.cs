public abstract class AttributedSaveSerializable : SaveSerializable
{
    private string _key;

    public AttributedSaveSerializable(string parentKey)
    {
        _key = $"{parentKey}_{SubKey}";
    }

    public abstract string SubKey { get; }

    public sealed override string Key => _key;
}
