using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnersInformationChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentLabelWave;
    [SerializeField] private TMP_Text _currentWaveText;
    [SerializeField] private TMP_Text _finishedWavesText;

    private Coroutine _writeLabelCoroutine;
    private Coroutine _eraseLabelCoroutine;

    public int WaveIndex { get; private set; }

    private void Awake()
    {
        _currentLabelWave.alpha = 0;
        WaveIndex = 1;
    }

    public void ChangeWaveInformation()
    {
        _currentWaveText.text = $"Current wave\n{WaveIndex}";
        _finishedWavesText.text = $"Finished waves - {WaveIndex - 1}";
    }

    public void ShowCurrentWaveLabel()
    {
        _writeLabelCoroutine = StartCoroutine(WriteCurrentWave());
    }

    public void IncreaseWaveIndex()
    {
        WaveIndex++;
    }

    private IEnumerator WriteCurrentWave()
    {
        _currentLabelWave.text = $"Current wave - {WaveIndex}";

        while (_currentLabelWave.alpha < 1)
        {
            _currentLabelWave.alpha += 0.7f * Time.deltaTime;
            yield return null;
        }

        _eraseLabelCoroutine = StartCoroutine(EraseCurrentWave());
        StopCoroutine(_writeLabelCoroutine);
    }

    private IEnumerator EraseCurrentWave()
    {
        while (_currentLabelWave.alpha > 0)
        {
            _currentLabelWave.alpha -= 0.7f * Time.deltaTime;
            yield return null;
        }

        StopCoroutine(_eraseLabelCoroutine);
    }
}
