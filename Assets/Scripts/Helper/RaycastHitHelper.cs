using UnityEngine;

public static class RaycastHitHelper
{
    public static T GetComponent<T>(this RaycastHit hit)
    {
        return hit.collider.gameObject.GetComponent<T>();
    }
}
