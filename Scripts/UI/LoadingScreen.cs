using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private Coroutine _coroutine;

    private void Start()
    {
        _icon.fillAmount = 0;
        _coroutine = StartCoroutine(FillIcon());
    }

    private IEnumerator FillIcon()
    {
        while (_icon.fillAmount != 1f)
        {
            _icon.fillAmount += Time.deltaTime;
            yield return null;
        }

        StopCoroutine(_coroutine);
    }
}
