using System;
using System.Collections;
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
    public event Action PreSave;

    private const string FILE_NAME = "save.json";

    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
#if UNITY_EDITOR
        Formatting = Formatting.Indented,
#endif
    };

    private Save Save { get; set; }

    public bool IsLoaded => Save != null;
    private string FilePath => $"{Application.persistentDataPath}/{FILE_NAME}";

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

    private void LoadFile()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            Save = JsonConvert.DeserializeObject<Save>(json, _settings);
        }
        else
        {
            Save = new Save();
        }
    }

    private void SaveFile()
    {
        PreSave?.Invoke();
        string json = JsonConvert.SerializeObject(Save, _settings);
        File.WriteAllText(FilePath, json);
    }

    protected override void OnAwake()
    {
        LoadFile();
    }

    protected void OnApplicationQuit()
    {
        SaveFile();
    }

    public static IEnumerator WaitUntilGameLoaded()
    {
        while (NotInitialized)
            yield return null;
        while (!Instance.IsLoaded)
            yield return null;
    }
}
