using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Goo.Tools.Pooling
{
    public interface IPooled
    {
        bool IsDisabled { get; set; }
        GameObject gameObject { get; }
    }

    public class Pooled : MonoBehaviour, IPooled
    {
        public bool IsDisabled { get; set; }
    }

    public static class IPooledHandler
    {
        public static void SetDisabled(this IPooled obj)
        {
            obj.IsDisabled = true;
            obj.gameObject.SetActive(false);
        }

        public static void SetEnabled(this IPooled obj)
        {
            obj.IsDisabled = false;
            obj.gameObject.SetActive(true);
        }
    }

    public class ExtendedObjectPooler : MonoBehaviour, IObjectPooler
    {
        public const string PREFIX_NAME = "[Pooling] ";

        [SerializeField] private GameObject _prefab;
        [Tooltip("If null parent will be created automatically")]
        [SerializeField] private Transform _parent;

        private readonly List<IPooled> _list = new List<IPooled>();

        public GameObject GetObject()
        {
            IPooled obj = null;

            foreach (var o in _list)
            {
                if (o.IsDisabled)
                {
                    obj = o;
                    break;
                }
            }

            if (obj == null)
            {
                var go = Instantiate(_prefab, _parent);
                obj = go.GetComponent<IPooled>();

                if (obj == null)
                {
                    var pooled = go.AddComponent<Pooled>();
                    pooled.hideFlags = HideFlags.HideInInspector;
                    obj = pooled;
                }

                _list.Add(obj);
            }

            obj.SetEnabled();
            return obj.gameObject;
        }

        public GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            var go = GetObject();
            go.transform.position = position;
            go.transform.rotation = rotation;
            return go;
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