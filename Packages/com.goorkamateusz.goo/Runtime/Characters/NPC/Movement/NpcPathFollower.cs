using UnityEngine;

namespace Goo.Characters.NPC.Movement
{
    public class NpcPathFollower : CharacterComponent
    {
        [SerializeField] protected CharacterMovement _movement;
        [SerializeField] protected NpcPath _path;
        [SerializeField] protected int _currentPoint;

        protected override void OnStart()
        {
            if (_path == null)
                enabled = false;
        }

        protected virtual void Update()
        {
            Transform nextPoint = _path[_currentPoint];
            Vector3 translation = nextPoint.position - Character.Position;
            float moveDistance = _movement.Speed * Time.deltaTime;

            if (moveDistance > translation.magnitude)
            {
                _movement.Move(translation);
                _currentPoint = _path.NextPointIndex(_currentPoint);
            }
            else
            {
                _movement.Move(translation.normalized * moveDistance);
            }
        }

        protected virtual void OnValidate()
        {
            if (_currentPoint > _path.Count)
                _currentPoint = _path.Count - 1;
        }
    }
}