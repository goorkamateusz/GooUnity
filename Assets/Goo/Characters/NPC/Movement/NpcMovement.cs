using UnityEngine;

namespace Assets.Goo.Characters.NPC.Movement
{
    public class NpcMovement : CharacterMovement
    {
        // todo npc movement
        public override float CurrentSpeed => Speed;

        public override void Move(Vector3 move)
        {
            Character.Position += move;
        }

        public override void Stop()
        {
            Pathfinding.ResetPath();
        }

        public override void Wrap(Vector3 position, Quaternion rotation)
        {
            _pathfinding.Agent.Warp(position);
            Character.Rotation = rotation;
        }

        protected override void HandleInput()
        {
        }

        protected override void OnSpeedChange()
        {
            _pathfinding.Agent.speed = Speed;
        }
    }
}