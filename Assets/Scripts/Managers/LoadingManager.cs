using System.Collections;
using Assets.Goo.Tools.Patterns;

public class LoadingManager : Singleton<LoadingManager>
{
    public IEnumerator WaitUntilGameLoaded()
    {
        // todo inject all of needed managers to wait like SaveManager, LoadingScreen, SceneSaves
        yield break;
    }
}
