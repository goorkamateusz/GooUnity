using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Goo.UnitTests
{
    public class MonoBehaviourInitializer<T> where T : Component
    {
        private T _component;
        private SerializedObject _serialized;

        private MonoBehaviourInitializer(T component)
        {
            _component = component;
            _serialized = new SerializedObject(component);
        }

        public MonoBehaviourInitializer<T> Set(string name, Action<SerializedProperty> action)
        {
            action(_serialized.FindProperty(name));
            return this;
        }

        public T Final()
        {
            _serialized.ApplyModifiedProperties();
            return _component;
        }

        public static MonoBehaviourInitializer<T> Instantiate()
        {
            var go = new GameObject();
            var t = go.AddComponent<T>();
            return new MonoBehaviourInitializer<T>(t);
        }
    }
}
