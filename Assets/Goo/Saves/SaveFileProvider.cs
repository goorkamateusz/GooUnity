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

        public Save Load()
        {
            return Load(Path);
        }

        public Save Load(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var save = JsonConvert.DeserializeObject<Save>(json, _settings);
                if (save != null)
                    return save;
            }
            return new Save();
        }

        public void Save(Save save)
        {
            Save(save, Path);
        }

        public void Save(Save save, string path)
        {
            string json = JsonConvert.SerializeObject(save, _settings);
            File.WriteAllText(path, json);
        }

        public void Delete()
        {
            Delete(Path);
        }

        public static void Delete(string path)
        {
            if (Exist(path))
                File.Delete(path);
        }

        public bool Exist()
        {
            return Exist(Path);
        }

        public static bool Exist(string path)
        {
            return File.Exists(path);
        }
    }
}