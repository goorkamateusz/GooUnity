using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Goo.Tools.Pooling;
using NUnit.Framework;

namespace Assets.GooTests.EditorMode.Pooling
{
    internal class TestablePooler : PoolingObjects, IMonoBehaviourTest
    {
        public bool IsTestFinished { get; set; }
    }

    internal class ExampleComponent : MonoBehaviour { }

    public abstract class PoolingObjectsTests
    {
        protected const string Name = "TestName";

        internal TestablePooler _pooler;
        protected GameObject _prefab;

        [SetUp]
        public abstract void SetUp();

        [TearDown]
        public virtual void TearDown()
        {
            _pooler.IsTestFinished = true;
        }

        [Test]
        public void GetObject_Base()
        {
            var actual = _pooler.GetObject();
            Assert.AreEqual($"{Name}(Clone)", actual.name);
        }

        [Test]
        public void GetObject_PrefabWithComponents()
        {
            _prefab.AddComponent<ExampleComponent>();
            var actual = _pooler.GetObject();
            Assert.IsNotNull(actual.transform.GetComponent<ExampleComponent>());
        }

        [Test]
        public void GetObject_Reusability()
        {
            var first = _pooler.GetObject();
            first.SetActive(false);
            var second = _pooler.GetObject();
            Assert.AreSame(first, second);
        }

        [Test]
        public void GetObject_NotInRoot()
        {
            var actual = _pooler.GetObject();
            Assert.NotNull(actual.transform.parent);
        }

        [Test]
        public void GetObject_SameParent()
        {
            var first = _pooler.GetObject();
            var second = _pooler.GetObject();
            Assert.AreSame(first.transform.parent, second.transform.parent);
        }

        [Test]
        public void GetObject_DefaultPosition()
        {
            Vector3 position = new Vector3(1, 2, 3);
            Quaternion rotation = Quaternion.Euler(4, 5, 6);
            var actual = _pooler.GetObject(position, rotation);
            Assert.AreEqual(position, actual.transform.position);
            Assert.AreEqual(rotation, actual.transform.rotation);
        }

        [Test]
        public void GetObject_MultipleGetsNewElements()
        {
            var history = new List<GameObject>();
            for (int i = 0; i < 5; i++)
            {
                var actual = _pooler.GetObject();
                Assert.IsFalse(history.Contains(actual), $"Id: {i}");
                history.Add(actual);
            }
        }

        [Test]
        public void GetObject_MultipleGetExistingElements()
        {
            const int Length = 5;
            var objects = new List<GameObject>(Length);

            for (int i = 0; i < Length; i++)
            {
                var actual = _pooler.GetObject();
                objects.Add(actual);
            }

            foreach (var item in objects)
            {
                item.SetActive(false);
            }

            for (int i = 0; i < Length; i++)
            {
                var actual = _pooler.GetObject();
                Assert.IsTrue(objects.Contains(actual), $"Id: {i}");
            }
        }
    }
}