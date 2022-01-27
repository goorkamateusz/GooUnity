using System.Collections;
using UnityEngine;

public class CharacterComponent : MonoBehaviour, ICharacterComponent
{
    protected Character Character { get; private set; }

    public void InjectCharacter(Character character)
    {
        Character = character;
    }

    protected virtual IEnumerator Start()
    {
        OnStart();
        enabled = false;
        yield return LoadingManager.Instance.WaitUntilGameLoaded();
        AfterGameLoaded();
        enabled = true;
    }

    protected virtual void OnStart() { }
    protected virtual void AfterGameLoaded() { }
}
