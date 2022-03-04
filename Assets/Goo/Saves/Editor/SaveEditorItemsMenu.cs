using UnityEditor;
using Assets.Goo.EditorTools.Editor;

namespace Goo.Saves.Editor
{
    public class SaveEditorItemsMenu : EditorWindowRelatedToManager<SaveManager>
    {
        private const string SAVES_MENU = ItemMenuNames.ROOT_MENU;
        private const string CLEAN_SAVE_WINDOW = SAVES_MENU + "Edit saves";
        private const string CLEAR_SAVE = SAVES_MENU + "Clear saves";

        [MenuItem(CLEAN_SAVE_WINDOW)]
        private static void OpenWindow() => EditorWindow.GetWindow<SaveEditor>();

        [MenuItem(CLEAN_SAVE_WINDOW, true)]
        private static bool ValidateOpenWindow() => ManagerExist && Manager.GetFileProvider().Exist();

        [MenuItem(CLEAR_SAVE)]
        private static void ClearSave() => Manager.GetFileProvider().Delete();

        [MenuItem(CLEAR_SAVE, true)]
        private static bool ValidateClearSave() => ManagerExist && Manager.GetFileProvider().Exist();
    }
}