using UnityEngine;

namespace Assets.Goo.EnviroTools
{
    public static class ChildAnalyser
    {
        public static (Vector3, Vector3) GetBox(Transform parent)
        {
            Vector3 min = Vector3.positiveInfinity, max = Vector3.negativeInfinity;
            Vector3 position;

            for (int i = 0; i < parent.childCount; i++)
            {
                position = parent.GetChild(i).position;

                if (min.x > position.x) min.x = position.x;
                if (min.y > position.y) min.y = position.y;
                if (min.z > position.z) min.z = position.z;

                if (max.x < position.x) max.x = position.x;
                if (max.y < position.y) max.y = position.y;
                if (max.z < position.z) max.z = position.z;
            }

            return (min, max);
        }

        public static (Vector2, Vector2) GetBox2D(Transform parent)
        {
            Vector2 min = Vector2.positiveInfinity, max = Vector2.negativeInfinity;
            Vector3 position;

            for (int i = 0; i < parent.childCount; i++)
            {
                position = parent.GetChild(i).position;

                if (min.x > position.x) min.x = position.x;
                if (min.y > position.z) min.y = position.z;

                if (max.x < position.x) max.x = position.x;
                if (max.y < position.z) max.y = position.z;
            }

            return (min, max);
        }
    }
}