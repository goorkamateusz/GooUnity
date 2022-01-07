using System;
using Newtonsoft.Json;

[Serializable]
public abstract class SaveSerializable
{
    private const string NoneVersion = "None";

    public string Version { get; set; }

    [JsonIgnore] public abstract string Key { get; }
    [JsonIgnore] public virtual string LatestVersion => NoneVersion;
    [JsonIgnore] public bool Initialized => Version != null;

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