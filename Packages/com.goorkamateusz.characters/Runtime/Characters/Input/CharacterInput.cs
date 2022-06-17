using UnityEngine;
using Goo.Characters.Interactions;

namespace Goo.Characters
{
    public class CharacterInput : MonoBehaviour
    {
        private readonly KeyInteractions _keys = new KeyInteractions();
        private readonly MouseInteractions _mouse = new MouseInteractions();

        public MouseInteractions MouseInteraction => _mouse;
        public KeyInteractions KeyInteractions => _keys;

        protected void Awake()
        {
            _mouse.SetCamera(Camera.main);
        }

        protected void Update()
        {
            _keys.CheckAll();
            _mouse.HandleInput();
        }

        protected void LateUpdate()
        {
            _mouse.HandleLateUpdate();
        }
    }
}