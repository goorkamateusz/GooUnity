using UnityEngine;
using UnityEngine.TestTools;
using Assets.Goo.Tools.Pooling;
using Assets.Goo.UnitTests;
using NUnit.Framework;

namespace Assets.GooTests.EditorMode.Pooling
{
    internal class ExtendedTestablePooler : ExtendedObjectPooler, IMonoBehaviourTest
    {
        public bool IsTestFinished { get; set; }
    }

    internal class CustomPooled : MonoBehaviour, IPooled
    {
        public bool IsDisabled { get; set; }
    }

    public class ExtendedObjectPoolerTests
    {
        private ExtendedTestablePooler _pooler;
        private GameObject _prefab;

        [SetUp]
        public virtual void SetUp()
        {
            _prefab = new GameObject("TestName");
            _pooler = MonoBehaviourInitializer<ExtendedTestablePooler>.Instantiate()
                .Set("_prefab", p => p.objectReferenceValue = _prefab)
                .Apply()
                .RunInEditor()
                .Get();
        }

        [TearDown]
        public virtual void TearDown()
        {
            _pooler.IsTestFinished = true;
        }

        [Test]
        public void GetObject_WithoutIPooled()
        {
            var obj = _pooler.GetObject();
            IPooled pooled = obj.GetComponent<IPooled>();
            Assert.IsNotNull(pooled);
            Assert.IsFalse(pooled.IsDisabled);
            Assert.IsTrue(obj.activeSelf);
        }

        [Test]
        public void GetObject_WithPooledComponent()
        {
            _prefab.AddComponent<Pooled>();
            var obj = _pooler.GetObject();
            IPooled pooled = obj.GetComponent<IPooled>();
            Assert.IsNotNull(pooled);
            Assert.IsFalse(pooled.IsDisabled);
            Assert.IsTrue(obj.activeSelf);
        }

        [Test]
        public void GetObject_WithCustomIPooled()
        {
            _prefab.AddComponent<CustomPooled>();
            var obj = _pooler.GetObject();
            IPooled pooled = obj.GetComponent<IPooled>();
            Assert.IsNotNull(pooled);
            Assert.IsFalse(pooled.IsDisabled);
            Assert.IsTrue(obj.activeSelf);
        }

        [Test]
        public void GetObject_Reusability()
        {
            var first = _pooler.GetObject();
            IPooled pooled1 = first.GetComponent<IPooled>();
            pooled1.IsDisabled = true;
            var second = _pooler.GetObject();
            IPooled pooled2 = second.GetComponent<IPooled>();
            Assert.AreSame(first, second);
            Assert.AreSame(pooled1, pooled2);
            Assert.IsFalse(pooled2.IsDisabled);
        }

        [Test]
        public void GetObject_ReusabilityOfIPooledBehaviour()
        {
            _prefab.AddComponent<CustomPooled>();
            var first = _pooler.GetObject();
            CustomPooled pooled1 = first.GetComponent<CustomPooled>();
            pooled1.SetDisabled();
            var second = _pooler.GetObject();
            IPooled pooled2 = second.GetComponent<IPooled>();
            Assert.AreSame(first, second);
            Assert.AreSame(pooled1, pooled2);
            Assert.IsFalse(pooled2.IsDisabled);
        }
    }
}