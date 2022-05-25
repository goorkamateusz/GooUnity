using System;
using UnityEditor;
using UnityEngine;
using Assets.Goo.Tools.Pooling;
using NUnit.Framework;

namespace Assets.GooTests.Tests.Pooling
{
    public class PoolingObjectsTests
    {
        //! 1 ----------
        [Test]
        public void GetObjectTests_Manual()
        {
            var objectToPool = new GameObject("test name");
            var gameObject = new GameObject();
            var pooler = gameObject.AddComponent<PoolingObjects>();
            var so = new SerializedObject(pooler);
            so.FindProperty("_prefab").objectReferenceValue = objectToPool;
            so.ApplyModifiedProperties();
            var actual = pooler.GetObject();
            Assert.AreEqual("test name(Clone)", actual.name);
        }

        //! 2 ----------
        [Test]
        public void GetObjectTests_TupleArray()
        {
            var objectToPool = new GameObject("test name");
            var pooler = MonoBehaviourTests.Initialize<PoolingObjects>(new (string, Action<SerializedProperty>)[]
            {
                ("_prefab", p => p.objectReferenceValue = objectToPool)
            });
            var actual = pooler.GetObject();
            Assert.AreEqual("test name(Clone)", actual.name);
        }

        //! 3 ----------
        [Test]
        public void GetObjectTests_StructArray()
        {
            var objectToPool = new GameObject("test name");
            var pooler = MonoBehaviourTests.Initialize<PoolingObjects>(new MonoBehaviourTests.Initializer[]
            {
                new MonoBehaviourTests.Initializer
                {
                    Name = "_prefab",
                    Action = p => p.objectReferenceValue = objectToPool
                }
            });
            var actual = pooler.GetObject();
            Assert.AreEqual("test name(Clone)", actual.name);
        }

        //! 4 ----------
        [Test]
        public void GetObjectTests_Chain()
        {
            var objectToPool = new GameObject("test name");
            var pooler = MonoBehaviourInitializer<PoolingObjects>.Instantiate()
                .Set("_prefab", p => p.objectReferenceValue = objectToPool)
                .Final();
            var actual = pooler.GetObject();
            Assert.AreEqual("test name(Clone)", actual.name);
        }
    }

    public static class MonoBehaviourTests
    {
        public static T Initialize<T>() where T : Component
        {
            var go = new GameObject();
            return go.AddComponent<T>();
        }

        public static T Initialize<T>((string, Action<SerializedProperty>)[] properties) where T : Component
        {
            T o = Initialize<T>();
            var so = new SerializedObject(o);

            foreach ((string name, var action) in properties)
            {
                action.Invoke(so.FindProperty(name));
            }

            so.ApplyModifiedProperties();
            return o;
        }

        public struct Initializer
        {
            public string Name;
            public Action<SerializedProperty> Action;
        }

        public static T Initialize<T>(Initializer[] properties) where T : Component
        {
            T o = Initialize<T>();
            var so = new SerializedObject(o);

            foreach (var p in properties)
            {
                p.Action.Invoke(so.FindProperty(p.Name));
            }

            so.ApplyModifiedProperties();
            return o;
        }

    }

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
