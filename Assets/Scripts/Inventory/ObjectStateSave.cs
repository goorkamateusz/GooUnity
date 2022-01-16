using System.Collections;
using UnityEngine;

public class ObjectStateSave : MonoBehaviour
{
    protected void SaveEnabled()
    {
        if (ObjectStateSaveManager.Ready)
            ObjectStateSaveManager.Instance.Enabled(gameObject);
    }

    protected void SaveDisabled()
    {
        if (ObjectStateSaveManager.Ready)
            ObjectStateSaveManager.Instance.Disabled(gameObject);
    }

    protected IEnumerator Start()
    {
        yield return ObjectStateSaveManager.LoadSave(gameObject);
    }

    protected virtual void OnEnable()
    {
        SaveEnabled();
    }

    protected virtual void OnDisable()
    {
        SaveDisabled();
    }
}
