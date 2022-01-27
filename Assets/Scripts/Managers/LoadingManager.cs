using System.Collections;
using Goo.Tools;

public class LoadingManager : Singleton<LoadingManager>
{
    public IEnumerator WaitUntilGameLoaded()
    {
        // todo inject all of needed managers to wait like SaveManager, LoadingScreen, SceneSaves
        yield break;
    }
}
