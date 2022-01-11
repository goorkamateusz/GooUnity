using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveEditor : EditorWindow
{
    private const string CLEAN_SAVE_WINDOW = "Playground/Edit saves";
    private const string CLEAR_SAVE = "Playground/Clear saves";

    private Save _save;
    private Dictionary<string, bool> _toggles = new Dictionary<string, bool>();

    private void Awake()
    {
        _save = SaveFileHelper.Load();
        foreach (var item in _save)
            _toggles[item.Key] = false;
    }

    private void OnGUI()
    {
        UpdateToggels();
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (GUILayout.Button("Delete selected"))
            DeleteSelected();
    }

    private void UpdateToggels()
    {
        foreach (var item in _save)
            _toggles[item.Key] = EditorGUILayout.Toggle(item.Key, _toggles[item.Key]);
    }

    private void DeleteSelected()
    {
        foreach (var toggle in _toggles)
        {
            if (toggle.Value)
                _save.Remove(toggle.Key);
        }
        SaveFileHelper.Save(_save);
    }

    [MenuItem(CLEAN_SAVE_WINDOW)]
    private static void OpenWindow() => EditorWindow.GetWindow<SaveEditor>();

    [MenuItem(CLEAN_SAVE_WINDOW, true)]
    private static bool ValidateOpenWindow() => SaveFileHelper.Exist();

    [MenuItem(CLEAR_SAVE)]
    private static void ClearSave() => SaveFileHelper.Delete();

    [MenuItem(CLEAR_SAVE, true)]
    private static bool ValidateClearSave() => SaveFileHelper.Exist();
}
