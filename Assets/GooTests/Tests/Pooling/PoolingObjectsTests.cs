using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Goo.Tools.Pooling;
using Assets.Goo.UnitTests;
using NUnit.Framework;

namespace Assets.GooTests.Tests.Pooling
{
    public class PoolingObjectsTests
    {
        private class ExampleComponent : MonoBehaviour { }

        private const string Name = "TestName";

        private PoolingObjects _pooler;
        private GameObject _prefab;

        [SetUp]
        public void SetUp()
        {
            _prefab = new GameObject(Name);
            _pooler = MonoBehaviourInitializer<PoolingObjects>.Instantiate()
                .Set("_prefab", p => p.objectReferenceValue = _prefab)
                .Final();
        }

        [Test]
        public void GetObject_Base()
        {
            var actual = _pooler.GetObject();
            Assert.AreEqual("TestName(Clone)", actual.name);
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

        [UnityTest]
        public IEnumerator GetObject_NotInRoot()
        {
            // todo do play mode test (Awake problems)
            var actual = _pooler.GetObject();
            yield return null;
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
    }
}
