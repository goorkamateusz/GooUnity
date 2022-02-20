using UnityEngine;

public class StairStep : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private Transform _nextStairHook;

    public Transform NextHook => _nextStairHook;

    private void SpawnNext(int number = 1)
    {
        if (number > 0)
        {
            for (int i = 0; i < NextHook.childCount; i++)
            {
                DestroyImmediate(NextHook.GetChild(i).gameObject);
            }

            var next = Instantiate(this);
            next.transform.parent = NextHook;
            next.transform.localPosition = Vector3.zero;

            next.SpawnNext(--number);
        }
    }

    [ContextMenu("Spawn 10")]
    private void SpawnTwenty() => SpawnNext(10);
#endif
}
