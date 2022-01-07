using System;
using Newtonsoft.Json;

[Serializable]
public abstract class SaveSerializable
{
    public string Version { get; set; }

    [JsonIgnore] public abstract string Key { get; }
    [JsonIgnore] public abstract string LatestVersion { get; }
    [JsonIgnore] public bool Initialized => Version != string.Empty;

    public SaveSerializable()
    {
        Version = null;
    }

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }

    public virtual void UpdateVersion()
    {
        if (Version != LatestVersion)
        {
            Migrations();
            Version = LatestVersion;
        }
    }

    protected virtual void Migrations()
    {
    }
}

public abstract class SaveSerializable<T> : SaveSerializable
{
    public T Data { get; set; }
}

public abstract class SaveListenerSerializable : SaveSerializable
{
    public abstract void PreSaveInvoke();
}

public abstract class SaveListenerSerializable<T> : SaveListenerSerializable where T : SaveListenerSerializable<T>
{
    public delegate void PreSaveDelegate(T obj);

    [JsonIgnore] public PreSaveDelegate PreSave;

    public override void PreSaveInvoke()
    {
        if (PreSave != null)
            PreSave(this as T);
    }
}

public abstract class CharacterSaveSerializable<T> : SaveListenerSerializable<T>  where T : SaveListenerSerializable<T>
{
    private string _key;

    public CharacterSaveSerializable(string parentKey)
    {
        _key = $"{parentKey}_{SubKey}";
    }

    public abstract string SubKey { get; }

    public sealed override string Key => _key;
}
