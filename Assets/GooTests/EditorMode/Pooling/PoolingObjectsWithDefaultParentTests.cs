using UnityEngine;
using Assets.Goo.UnitTests;
using NUnit.Framework;

namespace Assets.GooTests.EditorMode.Pooling
{
    public class PoolingObjectsWithDefaultParentTests : PoolingObjectsTests
    {
        public override void SetUp()
        {
            _prefab = new GameObject(Name);
            _pooler = MonoBehaviourInitializer<TestablePooler>.Instantiate()
                .Set("_prefab", p => p.objectReferenceValue = _prefab)
                .Apply()
                .RunInEditor()
                .Get();
        }

        [Test]
        public void GetObject_RootNameConvention()
        {
            var actual = _pooler.GetObject();
            Assert.IsTrue(actual.transform.parent?.name.StartsWith(TestablePooler.PREFIX_NAME));
        }
    }
}