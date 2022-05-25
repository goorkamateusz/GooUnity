using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Goo.Tools.Pooling;
using NUnit.Framework;

namespace Assets.GooTests.Tests.Pooling
{
    public class PoolingObjectsTests
    {
        public class TestablePoolingObjects : PoolingObjects, IMonoBehaviourTest
        {
            public bool IsTestFinished { get; set; }
        }

        //! 1 ----------
        [Test]
        public void GetObjectTests_Manual()
        {
            var objectToPool = new GameObject("test name");
            var gameObject = new GameObject();
            var pooler = gameObject.AddComponent<TestablePoolingObjects>();
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
            (var gameObject, var pooler) = MonoBehaviourTests.Initialize<PoolingObjects>(new (string, Action<SerializedProperty>)[]
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
            (var gameObject, var pooler) = MonoBehaviourTests.Initialize<PoolingObjects>(new MonoBehaviourTests.Initializer[]
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
            (var gameObject, var pooler) = MonoBehaviourTests.InitializeChain<PoolingObjects>()
                .Set("_prefab", p => p.objectReferenceValue = objectToPool)
                .Final();
            var actual = pooler.GetObject();
            Assert.AreEqual("test name(Clone)", actual.name);
        }
    }

    public static class MonoBehaviourTests
    {
        public static (GameObject, T) Initialize<T>() where T : Component
        {
            var go = new GameObject();
            return (go, go.AddComponent<T>());
        }

        public static (GameObject, T) Initialize<T>((string, Action<SerializedProperty>)[] properties) where T : Component
        {
            (GameObject go, T o) = Initialize<T>();
            var so = new SerializedObject(o);

            foreach ((string name, var action) in properties)
            {
                action.Invoke(so.FindProperty(name));
            }

            so.ApplyModifiedProperties();
            return (go, o);
        }

        public struct Initializer
        {
            public string Name;
            public Action<SerializedProperty> Action;
        }

        public static (GameObject, T) Initialize<T>(Initializer[] properties) where T : Component
        {
            (GameObject go, T o) = Initialize<T>();
            var so = new SerializedObject(o);

            foreach (var p in properties)
            {
                p.Action.Invoke(so.FindProperty(p.Name));
            }

            so.ApplyModifiedProperties();
            return (go, o);
        }

        public class InitializerChain<T> where T : Component
        {
            public (GameObject go, T o) _o;
            public SerializedObject _s;

            public InitializerChain((GameObject, T) obj)
            {
                _o = obj;
                (_, T o) = obj;
                _s = new SerializedObject(o);
            }

            public InitializerChain<T> Set(string name, Action<SerializedProperty> action)
            {
                action(_s.FindProperty(name));
                return this;
            }

            public (GameObject, T) Final()
            {
                _s.ApplyModifiedProperties();
                return _o;
            }
        }

        public static InitializerChain<T> InitializeChain<T>() where T : Component
        {
            var t = Initialize<T>();
            return new InitializerChain<T>(t);
        }
    }
}
