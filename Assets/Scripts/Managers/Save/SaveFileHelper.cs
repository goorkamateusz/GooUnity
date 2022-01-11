using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveFileHelper
{
    private const string FILE_NAME = "save.json";

    public static string Path => $"{Application.persistentDataPath}/{FILE_NAME}";

    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
#if UNITY_EDITOR
        Formatting = Formatting.Indented,
#endif
    };

    public static Save Load() => Load(Path);

    public static Save Load(string path)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Save>(json, _settings);
        }
        return new Save();
    }

    public static void Save(Save save) => Save(save, Path);

    public static void Save(Save save, string path)
    {
        string json = JsonConvert.SerializeObject(save, _settings);
        File.WriteAllText(path, json);
    }

    public static void Delete() => Delete(Path);

    public static void Delete(string path)
    {
        if (Exist(path))
            File.Delete(path);
    }

    public static bool Exist() => Exist(Path);

    public static bool Exist(string path)
    {
        return File.Exists(path);
    }
}
