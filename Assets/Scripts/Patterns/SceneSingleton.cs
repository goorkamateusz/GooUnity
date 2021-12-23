using System.Collections;
using UnityEngine;

public class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
{
    private static T _instance;

    public static T Instance => _instance;

    public static IEnumerator Wait()
    {
        while (_instance == null)
            yield return null;
    }

    protected void Awake()
    {
        _instance = this as T;
    }
}
