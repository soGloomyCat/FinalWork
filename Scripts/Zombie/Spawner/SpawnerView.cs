using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private Button _button;

    private ZombiesSpawner _zombiesSpawner;

    public event UnityAction ButtonPressed;

    private void OnEnable()
    {
        _button.gameObject.SetActive(false);
        _zombiesSpawner = GetComponent<ZombiesSpawner>();
        _zombiesSpawner.WaveFinished += ActivateButton;
        _button.onClick.AddListener(LaunchWave);
    }

    private void OnDisable()
    {
        _zombiesSpawner.WaveFinished -= ActivateButton;
        _button.onClick.RemoveListener(LaunchWave);
    }

    private void ActivateButton()
    {
        _button.gameObject.SetActive(true);
    }

    private void LaunchWave()
    {
        _button.gameObject.SetActive(false);
        ButtonPressed?.Invoke();
    }
}
