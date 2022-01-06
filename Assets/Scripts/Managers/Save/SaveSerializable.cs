using System;
using Newtonsoft.Json;

[Serializable]
public abstract class SaveSerializable
{
    public string Version { get; set; }

    [JsonIgnore] public abstract string Key { get; }
    [JsonIgnore] public bool Initialized => Version != string.Empty;

    public SaveSerializable()
    {
        Version = null;
    }

    public abstract SaveSerializable GetDefault();

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}

[Serializable]
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
