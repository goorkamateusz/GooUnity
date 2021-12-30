using System.Collections;
using System;
using UnityEngine;
using TMPro;

public class KeyActionView : MonoBehaviour
{
    [SerializeField] private TMP_Text _letter;
    [SerializeField] private TMP_Text _desc;
    
    private Coroutine _coroutine;

    public void DisplayTip(KeyCode key, string desc, float lifeTime = 3f)
    {
        _letter.text = key.ToString();
        _desc.text = desc;

        gameObject.SetActive(true);
        if (_coroutine == null)
            _coroutine = StartCoroutine(Countdown(lifeTime));
        else
            throw new NotImplementedException("Many invokes");
    }

    private IEnumerator Countdown(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
        _coroutine = null;
    }
}
