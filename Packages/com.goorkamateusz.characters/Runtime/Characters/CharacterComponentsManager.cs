using System;
using System.Collections.Generic;
using UnityEngine;

namespace Goo.Characters
{
    [Serializable]
    public class CharacterComponentsManager
    {
        [SerializeField] private GameObject[] _componentNodes;

        private readonly Dictionary<Type, ICharacterComponent> _cache = new Dictionary<Type, ICharacterComponent>();

        internal void InitializeComponents(Character character)
        {
            foreach (var node in _componentNodes)
            {
                var abilities = node.GetComponents<ICharacterComponent>();
                foreach (var ability in abilities)
                {
                    ability.InjectCharacter(character);
                    SaveReference(ability);
                }
            }

            void SaveReference(ICharacterComponent ability)
            {
                Type key = ability.GetType();
                if (!_cache.ContainsKey(key))
                    _cache.Add(key, ability);
                else
                    Debug.LogError($"Multiple components of same type {key}");
            }
        }

        public T GetComponent<T>() where T : class, ICharacterComponent
        {
            return _cache[typeof(T)] as T;
        }

#if UNITY_EDITOR
        internal void __FindComponents(Transform transform)
        {
            var nodes = new List<GameObject>();
            __AggregateChildrenWithComponent<ICharacterComponent>(transform, ref nodes);
            _componentNodes = nodes.ToArray();
        }

        private void __AggregateChildrenWithComponent<T>(Transform parent, ref List<GameObject> nodes)
        {
            if (parent.TryGetComponent<T>(out _))
                nodes.Add(parent.gameObject);

            for (int i = 0; i < parent.childCount; ++i)
                __AggregateChildrenWithComponent<T>(parent.GetChild(i), ref nodes);
        }
#endif
    }
}