using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Goo.Tools.EventSystem;
using Assets.Goo.Tools.UnityHelpers;

namespace Assets.Goo.SceneObjects.Characters.UI
{
    public class SceneInteractionsContainer : MonoBehaviour, IEventListener<SceneInteractiveElement.Event>
    {
        [SerializeField] private List<SceneInteractionView> _views;

        public void OnEvent(SceneInteractiveElement.Event e)
        {
            if (e.Hide) HideTip(e.Key);
            else DisplayTip(e.Key, e.Messsage);
        }

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

        protected void Awake()
        {
            this.SubscribeEvent();
        }

        protected void OnDestroy()
        {
            this.UnsubscribeEvent();
        }
    }
}