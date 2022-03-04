using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Goo.Characters.NPC.Movement
{
    public class NpcPath : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;

        public Transform this[int index] => _points[index];

        public int Count => _points.Count;

        public int NextPointIndex(int currentPoint)
        {
            return (currentPoint + 1) % Count;
        }

        protected virtual void Reset()
        {
            var points = transform.GetComponentsInChildren<Transform>();
            _points = points.Where(point => point != transform).ToList();
        }
    }
}