using Goo.Tools;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ResetTime()
    {
        Time.timeScale = 1f;
    }
}
