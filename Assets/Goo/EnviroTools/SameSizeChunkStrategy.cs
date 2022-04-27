using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Goo.EnviroTools
{
    [Serializable]
    internal class SameSizeChunkStrategy : EnviroChunkOrganizer.ChunkStrategy
    {
        [SerializeField] private Vector2Int _number;

        internal override void Chunk(Transform transform)
        {
            Vector2 border = Vector2.zero;
            List<Transform> chunks = new List<Transform>();

            (Vector3 min, Vector3 max) = GetBox(transform);
            (float horizontal, float vertical) = Step(min, max);

            for (int x = 0; x < _number.x; x++)
            {
                border.x = min.x + horizontal * (x + 1);

                for (int y = 0; y < _number.y; y++)
                {
                    border.y = min.z + vertical * (y + 1);

                    var chunk = new GameObject($"Chunk({x},{y})");
                    chunks.Add(chunk.transform);

                    for (int i = transform.childCount - 1; i >= 0; i--)
                    {
                        Transform child = transform.GetChild(i);
                        if (child.position.x <= border.x && child.position.z <= border.y)
                            child.parent = chunk.transform;
                    }
                }
            }

            foreach (var chunk in chunks)
                chunk.parent = transform;
        }

        private (float, float) Step(Vector3 min, Vector3 max)
        {
            float horizontal = (max.x - min.x) / ((float)_number.x);
            float vertical = (max.z - min.z) / ((float)_number.y);
            return (horizontal, vertical);
        }

        internal override void DrawGizmos(Transform transform)
        {
            Gizmos.color = Color.red;

            (Vector3 min, Vector3 max) = GetBox(transform);
            float horizontal = (max.x - min.x) / ((float)_number.x);
            float vertical = (max.z - min.z) / ((float)_number.y);

            Vector3 start = min;
            start.y = 0;
            Vector3 finish = min;
            finish.y = 0;
            finish.z = max.z;

            for (int i = 0; i < _number.x + 1; i++)
            {
                Gizmos.DrawLine(start, finish);
                start.x += horizontal;
                finish.x += horizontal;
            }

            start = min;
            start.y = 0;
            finish = min;
            finish.y = 0;
            finish.x = max.x;

            for (int i = 0; i < _number.y + 180f; i++)
            {
                Gizmos.DrawLine(start, finish);
                start.z += vertical;
                finish.z += vertical;
            }
        }

        private static (Vector3, Vector3) GetBox(Transform transform)
        {
            Vector3 min = Vector3.positiveInfinity;
            Vector3 max = Vector3.negativeInfinity;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Vector3 position = child.position;

                if (min.x > position.x) min.x = position.x;
                if (min.y > position.y) min.y = position.y;
                if (min.z > position.z) min.z = position.z;

                if (max.x < position.x) max.x = position.x;
                if (max.y < position.y) max.y = position.y;
                if (max.z < position.z) max.z = position.z;
            }

            return (min, max);
        }
    }
}