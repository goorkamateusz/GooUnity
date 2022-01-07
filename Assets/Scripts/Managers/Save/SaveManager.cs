using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class Save : Dictionary<string, SaveSerializable>
{
}

public class SaveManager : SceneSingleton<SaveManager>
{
    private const string FILE_NAME = "save.json";

    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented
    };

    public Save Save { get; set; }

    public string FilePath => $"{Application.persistentDataPath}/{FILE_NAME}";

    public void LoadFile()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            Debug.Log(json);
            Save = JsonConvert.DeserializeObject<Save>(json, _settings);
            Debug.Log(Save);
        }
    }

    public void SaveFile()
    {
        PreSave();
        string json = JsonConvert.SerializeObject(Save, _settings);
        File.WriteAllText(FilePath, json);
    }

    private void PreSave()
    {
        foreach (var serializable in Save.Values)
        {
            var listener = serializable as SaveListenerSerializable;
            if (listener != null)
                listener.PreSaveInvoke();
        }
    }

    public T Load<T>(T test) where T : SaveSerializable
    {
        string key = test.Key;
        if (!Save.ContainsKey(key))
            Save[key] = test;
        Save[key].UpdateVersion();
        return Save[key] as T;
    }

    public void Load<T>(ref T test) where T : SaveSerializable
    {
        test = Load<T>(test);
    }

    protected override void Awake()
    {
        LoadFile();
        base.Awake();
    }

    protected void OnApplicationQuit()
    {
        SaveFile();
    }
}
