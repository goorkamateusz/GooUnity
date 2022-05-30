using UnityEngine;
using Goo.SceneObjects.OpenMechanism.Objects;

namespace Goo.SceneObjects.Characters
{
    public class AutoDoorInteraction : AutoSceneInteractionDefinitions<DoorBase>
    {
        [SerializeField] private bool _interactWithAutoDoors = true;
        [SerializeField] private bool _autoOpenClose;

        protected override bool OnEnter(DoorBase door, AutoSceneInteractionAbility ability)
        {
            if (door.AutoOpen)
            {
                return _interactWithAutoDoors;
            }
            else
            {
                if (!door.IsOpen && _autoOpenClose)
                    door.OnKeyDown(ability);
                return _autoOpenClose;
            }
        }

        protected override bool OnExit(DoorBase door, AutoSceneInteractionAbility ability)
        {
            if (door.AutoClose)
            {
                return _interactWithAutoDoors;
            }
            else
            {
                if (door.IsOpen && _autoOpenClose)
                    door.OnKeyDown(ability);
                return _autoOpenClose;
            }
        }
    }
}