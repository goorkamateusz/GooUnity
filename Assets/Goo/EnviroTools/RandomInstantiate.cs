using UnityEngine;

namespace Assets.Goo.EnviroTools
{
    public class RandomInstantiate : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Vector3 _radius;
        [SerializeField] private int _number;

        [ContextMenu("Rand")]
        public void Rand()
        {
            for (int i = 0; i < _number; i++)
            {
                InstantiateRandom();
            }
        }

        [ContextMenu("Delete")]
        public void Delete()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private void InstantiateRandom()
        {
            int prefabId = Random.Range(0, _prefabs.Length);
            GameObject prefab = _prefabs[prefabId];
            Vector3 scale = RandomScale();
            Vector3 eulerScale = RandomScale();
            GameObject go = Instantiate(prefab, transform.position + Vector3.Scale(_radius, scale), Quaternion.Euler(eulerScale * 180f));
            go.transform.parent = transform;
        }

        private static Vector3 RandomScale()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}