using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Goo.Tools.Pooling
{
    public class ObjectPooler : MonoBehaviour, IObjectPooler
    {
        public const string PREFIX_NAME = "[Pooling] ";

        [SerializeField] private GameObject _prefab;
        [Tooltip("If null parent will be created automatically")]
        [SerializeField] private Transform _parent;

        private List<GameObject> _list = new List<GameObject>();

        public virtual GameObject GetObject()
        {
            GameObject obj = null;
            foreach (var o in _list)
            {
                if (!o.activeSelf)
                {
                    obj = o;
                    break;
                }
            }

            if (obj == null)
            {
                obj = Instantiate(_prefab, _parent);
                _list.Add(obj);
            }

            obj.SetActive(true);
            return obj;
        }

        public virtual GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            var obj = GetObject();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        public virtual T GetObject<T>()
        {
            GameObject obj = GetObject();
            return obj.GetComponent<T>();
        }

        public virtual void DisableAll()
        {
            foreach (var item in _list)
                item.SetActive(false);
        }

        public virtual void DestroyAll()
        {
            foreach (var item in _list)
                Destroy(item);
        }

        protected virtual void Awake()
        {
            if (_prefab == null)
                throw new NullReferenceException("Prefab to pool is null");

            if (_parent == null)
            {
                var parent = new GameObject($"{PREFIX_NAME}{_prefab.name}");
                _parent = parent.transform;
            }
        }
    }
}