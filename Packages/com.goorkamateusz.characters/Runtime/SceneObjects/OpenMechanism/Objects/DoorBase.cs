using UnityEngine;
using UnityEngine.AI;

namespace Goo.SceneObjects.OpenMechanism.Objects
{
    public class DoorBase : OpenMechanismInteractive
    {
        [Tooltip("If not null it disable obstacle for openned door")]
        [SerializeField] private NavMeshObstacle _obstacle;

        protected override void OnStateChange()
        {
            if (_obstacle != null)
                _obstacle.enabled = !IsOpen;
        }
    }
}