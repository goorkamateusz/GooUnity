using System.Collections;
using Assets.Goo.Tools.Patterns;

public class LoadingManager : Singleton<LoadingManager>
{
    // ??? is it a good pattern?
    public IEnumerator WaitUntilGameLoaded()
    {
        // todo inject all of needed managers to wait like SaveManager, LoadingScreen, SceneSaves
        yield break;
    }
}
