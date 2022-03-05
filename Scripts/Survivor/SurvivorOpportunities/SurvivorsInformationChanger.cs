using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurvivorsInformationChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _killedZombiesText;
    [SerializeField] private TMP_Text _currentBalanceText;
    [SerializeField] private TMP_Text _killedZombiesResultText;
    
    private void OnEnable()
    {
        ChangeResultsInfo();
    }

    public void ChangeResultsInfo(int balance = 0, int killedZombies = 0)
    {
        _currentBalanceText.text = $"Balance\n{balance}";
        _killedZombiesText.text = $"Killed Zombies\n{killedZombies}";
        _killedZombiesResultText.text = $"Killed Zombies - {killedZombies}";
    }
}
