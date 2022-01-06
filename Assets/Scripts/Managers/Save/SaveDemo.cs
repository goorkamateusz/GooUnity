using System.Collections;
using UnityEngine;

public class TestSerializable : SaveSerializable
{
    public string TestString;
    public int TestInt;

    public override string Key => "testKeyName";

    public override SaveSerializable GetDefault()
    {
        return new TestSerializable
        {
            Version = "1.2",
            TestString = "Init value",
            TestInt = 1
        };
    }
}

public class SaveDemo : MonoBehaviour
{
    private TestSerializable _test = new TestSerializable();

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
