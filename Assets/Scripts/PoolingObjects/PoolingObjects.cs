using UnityEngine;

public class PoolingObjects<T> : MonoBehaviour where T : Object
{
    // todo mock
    [SerializeField] private T _prefab;

    public T GetObject()
    {
        return Instantiate<T>(_prefab);
    }

    public T GetObject(Vector3 position, Quaternion rotation)
    {
        return Instantiate<T>(_prefab, position, rotation);
    }
}

public class PoolingObjects : PoolingObjects<GameObject>
{

}
