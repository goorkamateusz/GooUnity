using System;
using System.Collections;
using System.Collections.Generic;

namespace Goo.Saves
{
    [Serializable]
    public class Save : Dictionary<string, SaveSerializable>
    {
    }

    public class SaveManager : SceneSingleton<SaveManager>
    {
        public event Action PreSave;

        private Save Save { get; set; }

        public bool IsLoaded => Save != null;

        public T Load<T>(T test) where T : SaveSerializable
        {
            string key = test.Key;
            if (!Save.ContainsKey(key))
                Save[key] = test;
            Save[key].ReportLoaded();
            return Save[key] as T;
        }

        public void Load<T>(ref T test) where T : SaveSerializable
        {
            test = Load<T>(test);
        }

        private void LoadFile()
        {
            Save = SaveFileHelper.Load();
        }

        private void SaveFile()
        {
            PreSave?.Invoke();
            SaveFileHelper.Save(Save);
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
}
