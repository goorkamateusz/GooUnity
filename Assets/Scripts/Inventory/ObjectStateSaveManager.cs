using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateSaveManager : SceneSingleton<ObjectStateSaveManager>
{
    private class Data : SaveSerializable
    {
        public List<string> Disabled = new List<string>();
        public override string Key => "ObjectStates";
    }

    private Data _data;

    public static bool Ready => Initialized && Instance._data != null;

    public void Load(GameObject gameObject)
    {
        bool active = !_data.Disabled.Contains(gameObject.name);
        gameObject.SetActive(active);
    }

    public static IEnumerator LoadSave(GameObject gameObject)
    {
        while (!Ready)
            yield return null;
        Instance.Load(gameObject);
    }

    public void Enabled(GameObject gameObject)
    {
        _data.Disabled.Remove(gameObject.name);
    }

    public void Disabled(GameObject gameObject)
    {
        _data.Disabled.Add(gameObject.name);
    }

    protected IEnumerator Start()
    {
        yield return SaveManager.Wait();
        _data = new Data();
        SaveManager.Instance.Load(ref _data);
    }
}
