using UnityEngine.TestTools;
using Assets.Goo.Tools.Pooling;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Assets.GooTests.Tests.Pooling
{
    public class PoolingObjectsTests
    {
        public class TestablePoolingObjects : PoolingObjects, IMonoBehaviourTest
        {
            public bool IsTestFinished { get; set; }
        }

        [UnityTest]
        public IEnumerator GetObjectTests()
        {
            var objectToPool = new GameObject("adaw");
            var pooler = new TestablePoolingObjects();
            var so = new SerializedObject(pooler);
            so.FindProperty("_prefab").objectReferenceValue = objectToPool;
            var a = pooler.GetObject();
yield return null;
            // Given
            // When
            // Then
        }
    }
}