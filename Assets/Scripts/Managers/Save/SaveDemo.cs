using System.Collections;
using UnityEngine;

public class TestSerializable : SaveSerializable
{
    public string TestString;
    public int TestInt;

    public override string Key => "testKeyName";
    public override string LatestVersion => "0.0.0";
}

public class SaveDemo : MonoBehaviour
{
    private TestSerializable _test = new TestSerializable
    {
        TestString = "Init value",
        TestInt = 1
    };

    IEnumerator Start()
    {
        yield return SaveManager.Wait();
        SaveManager.Instance.Load(ref _test);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _test.TestInt++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _test.TestString = $"{_test.TestString}.";
        }
    }
}