using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class KeyActionTipView : MonoBehaviour
{
    [SerializeField] private TMP_Text _letter;
    [SerializeField] private TMP_Text _desc;

    private Coroutine _coroutine;
    private KeyCode _key;

    public KeyCode Key => _key;
    public bool IsActive => gameObject.activeSelf;

    public void DisplayTip(KeyCode key, string desc, float lifeTime)
    {
        _letter.text = key.ToString();
        _desc.text = desc;
        _key = key;

        gameObject.SetActive(true);
        if (_coroutine == null)
            _coroutine = StartCoroutine(Countdown(lifeTime));
        else
            throw new NotImplementedException("Many invokes");
    }

    public void HideTip()
    {
        gameObject.SetActive(false);
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Countdown(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
        _coroutine = null;
    }
}
