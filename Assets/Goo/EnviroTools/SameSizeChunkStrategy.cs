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

            (Vector3 min, Vector3 max) = ChildAnalyser.GetBox(transform);
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

        internal override void DrawGizmos(Transform transform)
        {
            Gizmos.color = Color.red;
            Vector3 start, finish;

            (Vector3 min, Vector3 max) = ChildAnalyser.GetBox(transform);
            (float horizontal, float vertical) = Step(min, max);

            min.y = 0f;
            max.y = 0f;

            start = min;
            finish = min;
            finish.z = max.z;

            for (int i = 0; i < _number.x + 1; i++)
            {
                Gizmos.DrawLine(start, finish);
                start.x += horizontal;
                finish.x += horizontal;
            }

            start = min;
            finish = min;
            finish.x = max.x;

            for (int i = 0; i < _number.y + 1; i++)
            {
                Gizmos.DrawLine(start, finish);
                start.z += vertical;
                finish.z += vertical;
            }
        }

        private (float, float) Step(Vector3 min, Vector3 max)
        {
            float horizontal = (max.x - min.x) / ((float)_number.x);
            float vertical = (max.z - min.z) / ((float)_number.y);
            return (horizontal, vertical);
        }

        private (float, float) Step(Vector2 min, Vector2 max)
        {
            float horizontal = (max.x - min.x) / ((float)_number.x);
            float vertical = (max.y - min.y) / ((float)_number.y);
            return (horizontal, vertical);
        }
    }
}