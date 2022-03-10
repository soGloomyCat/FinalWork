using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelsController : MonoBehaviour
{
    [SerializeField] private Base _targetBase;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _GameOverPanel;
    [SerializeField] private GameObject _restartPanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _secondExitButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _pauseCloseButton;
    [SerializeField] private Button _shopCloseButton;
    [SerializeField] private Button _nextWaveButton;
    [SerializeField] private AudioSource _gameAudio;
    [SerializeField] private AudioSource _gameOverAudio;

    private void OnEnable()
    {
        _targetBase.Destroyed += OpenGameOverPanel;
        _pausePanel.SetActive(false);
        _shopPanel.SetActive(false);
        _GameOverPanel.SetActive(false);
        _restartPanel.SetActive(false);
        _pauseButton.onClick.AddListener(OpenPauseMenu);
        _shopButton.onClick.AddListener(OpenShopMenu);
        _exitButton.onClick.AddListener(ExitGame);
        _secondExitButton.onClick.AddListener(ExitGame);
        _restartButton.onClick.AddListener(RestartGame);
        _pauseCloseButton.onClick.AddListener(ClosePauseMenu);
        _shopCloseButton.onClick.AddListener(CloseShopMenu);
    }

    private void OnDisable()
    {
        _targetBase.Destroyed -= OpenGameOverPanel;
        _pauseButton.onClick.RemoveListener(OpenPauseMenu);
        _shopButton.onClick.RemoveListener(OpenShopMenu);
        _exitButton.onClick.RemoveListener(ExitGame);
        _secondExitButton.onClick.RemoveListener(ExitGame);
        _restartButton.onClick.RemoveListener(RestartGame);
        _pauseCloseButton.onClick.RemoveListener(ClosePauseMenu);
        _shopCloseButton.onClick.RemoveListener(CloseShopMenu);
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0;
        _pauseButton.interactable = false;
        _nextWaveButton.interactable = false;
        _pausePanel.SetActive(true);
    }

    private void OpenShopMenu()
    {
        _pausePanel.SetActive(false);
        _shopPanel.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        Time.timeScale = 1;
        _pauseButton.interactable = true;
        _nextWaveButton.interactable = true;
        _pausePanel.SetActive(false);
    }

    private void CloseShopMenu()
    {
        _pausePanel.SetActive(true);
        _shopPanel.SetActive(false);
    }

    private void OpenGameOverPanel()
    {
        Time.timeScale = 0;
        _GameOverPanel.SetActive(true);
        _gameAudio.Stop();
        _gameOverAudio.Play();
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        _restartPanel.SetActive(true);
        Invoke(nameof(ReloadScene), 1.5f);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ReloadScene()
    {
        _GameOverPanel.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
