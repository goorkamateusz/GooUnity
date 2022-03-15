using System.Collections.Generic;
using UnityEngine;

namespace Assets.Goo.Tools.PoolingObjects
{
    public class PoolingObjects : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [Tooltip("If null parent will be created automatically")]
        [SerializeField] private Transform _parent;

        private List<GameObject> _list = new List<GameObject>();

        public T GetObject<T>()
        {
            GameObject obj = GetObject();
            return obj.GetComponent<T>();
        }

        public GameObject GetObject()
        {
            GameObject obj = null;
            foreach (var o in _list)
            {
                if (!o.activeSelf)
                    obj = o;
            }

            if (obj == null)
            {
                obj = Instantiate(_prefab, _parent);
                _list.Add(obj);
            }

            obj.SetActive(true);
            return obj;
        }

        public void DisableAll()
        {
            foreach (var item in _list)
                item.SetActive(false);
        }

        public void DestroyAll()
        {
            foreach (var item in _list)
                Destroy(item);
        }

        public GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            var obj = GetObject();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        protected void Awake()
        {
            if (_parent == null)
            {
                var parent = new GameObject($"[Pooling] {_prefab.name}");
                _parent = parent.transform;
            }
        }
    }
}