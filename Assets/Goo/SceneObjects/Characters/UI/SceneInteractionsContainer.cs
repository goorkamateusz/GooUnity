using System.Collections.Generic;
using System.Linq;
using Assets.Goo.Tools.UnityHelpers;
using UnityEngine;

namespace Assets.Goo.SceneObjects.Characters.UI
{
    public class SceneInteractionsContainer : MonoBehaviour
    {
        [SerializeField] private List<SceneInteractionView> _views;

        public void DisplayTip(KeyCode key, string desc)
        {
            DisplayTip(key, desc, null);
        }

        public void DisplayTip(KeyCode key, string desc, float lifeTime)
        {
            DisplayTip(key, desc, lifeTime);
        }

        private void DisplayTip(KeyCode key, string desc, float? lifeTime)
        {
            var view = GetView(key) ?? GetInactive();
            if (view != null)
                view.DisplayTip(key, desc, lifeTime);
            else
                gameObject.LogError("Not avaliable scene interacion view");
        }

        public void HideTip(KeyCode key)
        {
            var view = GetView(key);
            if (view != null)
            {
                view.HideTip();
            }
        }

        private int EmptySlots()
        {
            return _views.Count - _views.Count((view) => view.IsActive);
        }

        private SceneInteractionView GetInactive()
        {
            return _views.FirstOrDefault((view) => !view.IsActive);
        }

        private SceneInteractionView GetView(KeyCode key)
        {
            return _views.FirstOrDefault((view) => view.Key == key);
        }
    }
}