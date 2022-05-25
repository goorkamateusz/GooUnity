using UnityEngine;

namespace Assets.Goo.Tools.Pooling
{
    public interface IObjectPooler
    {
        public GameObject GetObject();
        public GameObject GetObject(Vector3 position, Quaternion rotation);
    }
}