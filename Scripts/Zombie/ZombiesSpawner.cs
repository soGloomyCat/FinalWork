using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZombiesSpawner : MonoBehaviour
{
    [SerializeField] private List<Zombie> _zombies;
    [SerializeField] private int _startCountZombies;
    [SerializeField] private int _leftmostPosition;
    [SerializeField] private int _rightmostPosition;
    [SerializeField] private Transform _pool;
    [SerializeField] private Base _target;
    [SerializeField] private Hero _survivor;
    [SerializeField] private TMP_Text _currentLabelWave;
    [SerializeField] private TMP_Text _currentWaveText;
    [SerializeField] private TMP_Text _finishedWavesText;
    [SerializeField] private Button _button;

    private int _stepValueIncrease;
    private int _currentCountZombies;
    private int _waveIndex;
    private float _multiplierHealth;
    private float _stepMultiplierIncrease;
    private List<Zombie> _currentWave;
    private Coroutine _zombiesSpawnCoroutine;
    private Coroutine _writeLabelCoroutine;
    private Coroutine _eraseLabelCoroutine;

    private void OnEnable()
    {
        _button.onClick.AddListener(CreateNewWave);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(CreateNewWave);
    }

    private void Start()
    {
        _currentCountZombies = _startCountZombies;
        _waveIndex = 1;
        _multiplierHealth = 1;
        _stepMultiplierIncrease = 0.2f;
        _currentLabelWave.alpha = 0;
        CreateNewWave();
    }

    private void Update()
    {
        if (CheckAliveZombies())
            RestartSpawner();
    }

    private void ClearPool()
    {
        for (int i = 0; i < _pool.childCount; i++)
        {
            Destroy(_pool.GetChild(i).gameObject);
        }
    }

    private bool CheckAliveZombies()
    {
        for (int i = 0; i < _pool.childCount; i++)
        {
            if (_pool.GetChild(i).gameObject.activeSelf == true)
                return false;
        }

        return true;
    }

    private void CreateNewWave()
    {
        Zombie tempZombie;
        List<Zombie> tempZombiesList;
        int currentCapaity;

        _button.gameObject.SetActive(false);
        currentCapaity = _waveIndex;
        tempZombiesList = CreateTempList(_zombies);
        _currentWave = new List<Zombie>(_waveIndex);
        _currentWaveText.text = $"Current wave\n{_waveIndex}";
        _finishedWavesText.text = $"Finished waves - {_waveIndex - 1}";

        _writeLabelCoroutine = StartCoroutine(WriteCurrentWave());

        if (_pool.childCount > 0)
            ClearPool();

        if (_waveIndex >= _zombies.Count)
            currentCapaity = _zombies.Count;

        for (int i = 0; i < currentCapaity; i++)
        {
            tempZombie = tempZombiesList[Random.Range(0, tempZombiesList.Count)];
            tempZombiesList.Remove(tempZombie);
            _currentWave.Add(tempZombie);
        }

        if (_waveIndex % 2 != 0 && _waveIndex != 1)
            IncreaseMultiplier();
        
        _zombiesSpawnCoroutine = StartCoroutine(SpawnZombies());
    }

    private void RestartSpawner()
    {
        _button.gameObject.SetActive(true);
    }

    private void OnZombieDying(Zombie zombie)
    {
        zombie.Dying -= OnZombieDying;
        _survivor.AddMoney(zombie.Reward);
    }

    private List<Zombie> CreateTempList(List<Zombie> mainList)
    {
        List<Zombie> tempZombiesList;

        tempZombiesList = new List<Zombie>();

        foreach (var zombie in mainList)
        {
            tempZombiesList.Add(zombie);
        }

        return tempZombiesList;
    }

    private void IncreaseMultiplier()
    {
        _multiplierHealth += _stepMultiplierIncrease;
    }
    
    private IEnumerator WriteCurrentWave()
    {
        _currentLabelWave.text = $"Current wave - {_waveIndex}";

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

    private IEnumerator SpawnZombies()
    {
        WaitForSeconds timer = new WaitForSeconds(2f);
        Zombie tempZombie;
        Vector2 spawnPoint;

        for (int i = 0; i < _currentCountZombies; i++)
        {
            spawnPoint = new Vector2(Random.Range(_leftmostPosition, _rightmostPosition), _pool.position.y);
            tempZombie = Instantiate(_currentWave[Random.Range(0, _currentWave.Count)], spawnPoint, Quaternion.identity, _pool).GetComponent<Zombie>();
            tempZombie.Dying += OnZombieDying;
            tempZombie.InitTarget(_target);
            tempZombie.SetColor();
            tempZombie.IncreaseHealth(_multiplierHealth);
            yield return timer;
        }

        _waveIndex++;
        _stepValueIncrease = Random.Range(3, 8);
        _currentCountZombies += _stepValueIncrease;
        StopCoroutine(_zombiesSpawnCoroutine);
    }
}
