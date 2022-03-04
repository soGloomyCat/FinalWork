using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _loadPanel.SetActive(false);
        _startButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void StartGame()
    {
        _loadPanel.SetActive(true);
        _startPanel.SetActive(false);
        GameScene.Load();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
