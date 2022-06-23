using UnityEngine;
using Goo.Characters.Interactions;

namespace Goo.Characters
{
    public class CharacterInput : MonoBehaviour
    {
        public MouseInteractions MouseInteraction { get; protected set; }
        public KeyInteractions KeyInteractions { get; protected set; }

        protected CharacterInput()
        {
            Initialize();
        }

        protected void Awake()
        {
            MouseInteraction.SetCamera(Camera.main);
        }

        protected void Update()
        {
            KeyInteractions.CheckAll();
            MouseInteraction.HandleInput();
        }

        protected void LateUpdate()
        {
            MouseInteraction.HandleLateUpdate();
        }

        protected virtual void Initialize()
        {
            KeyInteractions = new KeyInteractions();
            MouseInteraction = new MouseInteractions();
        }
    }
}