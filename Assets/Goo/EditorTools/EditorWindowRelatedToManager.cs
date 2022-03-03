using UnityEditor;
using UnityEngine;

namespace Goo.EditorTools
{
    public class EditorWindowRelatedToManager<T> : EditorWindow where T : MonoBehaviour
    {
        private static T manager;

        protected static T Manager
        {
            get
            {
                if (!manager)
                    manager = FindObjectOfType<T>(true);
                return manager;
            }
        }

        protected static bool ManagerExist => Manager;
    }
}