using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Goo.Saves
{
    public class SaveFileProvider
    {
        private const string FILE_NAME = "save.json";

        [Obsolete]
        public static string Path => $"{Application.persistentDataPath}/{FILE_NAME}";

        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
#if UNITY_EDITOR
            Formatting = Formatting.Indented,
#endif
        };

        private static SaveFileProvider _instance; // todo temp

        [Obsolete]
        public static Save Load()
        {
            if (_instance == null)
                _instance = new SaveFileProvider();
            return _instance.Load(Path);
        }

        public Save Load(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Save>(json, _settings);
            }
            return new Save();
        }

        [Obsolete]
        public static void Save(Save save)
        {
            if (_instance == null)
                _instance = new SaveFileProvider();
            _instance.Save(save, Path);
        }

        public void Save(Save save, string path)
        {
            string json = JsonConvert.SerializeObject(save, _settings);
            File.WriteAllText(path, json);
        }

        [Obsolete]
        public static void Delete()
        {
            if (_instance == null)
                _instance = new SaveFileProvider();
            _instance.Delete(Path);
        }

        public void Delete(string path)
        {
            if (Exist(path))
                File.Delete(path);
        }

        [Obsolete]
        public static bool Exist()
        {
            if (_instance == null)
                _instance = new SaveFileProvider();
            return _instance.Exist(Path);
        }

        public bool Exist(string path)
        {
            return File.Exists(path);
        }
    }
}