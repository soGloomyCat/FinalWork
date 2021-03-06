using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadPanel : MonoBehaviour
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
        AsyncOperation asyncLoader = SceneManager.LoadSceneAsync(1);

        asyncLoader.allowSceneActivation = false;

        while (_icon.fillAmount != 1f && !asyncLoader.isDone)
        { 
            _icon.fillAmount += asyncLoader.progress * Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        asyncLoader.allowSceneActivation = true;
        StopCoroutine(_coroutine);
    }
}
