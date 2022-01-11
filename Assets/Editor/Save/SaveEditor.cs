using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class SaveEditor : EditorWindow
{
    private const string CLEAN_SAVE_WINDOW = "Playground/Edit saves";
    private const string CLEAR_SAVE = "Playground/Clear saves";

    private Save _saves;
    private Dictionary<string, bool> _toggles = new Dictionary<string, bool>();
    private IEnumerable<string> _sortedKeys;
    private bool _showSorted;

    public IEnumerable<string> Keys => _showSorted ? _sortedKeys : _saves.Keys as IEnumerable<string>;

    private void Awake()
    {
        _saves = SaveFileHelper.Load();
        foreach (var item in _saves)
            _toggles[item.Key] = false;
        UpdateKeys();
    }

    private void OnGUI()
    {
        UpdateToggels();
        UpdateButtons();
        UpdateSettings();
    }

    private void UpdateSettings()
    {
        _showSorted = EditorGUILayout.Toggle("Show key sorted", _showSorted);
    }

    private void UpdateButtons()
    {
        if (GUILayout.Button("Delete selected"))
            DeleteSelected();
    }

    private void UpdateToggels()
    {
        foreach (var key in Keys)
            _toggles[key] = EditorGUILayout.Toggle(key, _toggles[key]);
    }

    private void UpdateKeys()
    {
        _sortedKeys = _saves.Keys.OrderBy((key) => key).ToList();
    }

    private void DeleteSelected()
    {
        foreach (var toggle in _toggles)
        {
            if (toggle.Value)
                _saves.Remove(toggle.Key);
        }
        SaveFileHelper.Save(_saves);
        UpdateKeys();
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
