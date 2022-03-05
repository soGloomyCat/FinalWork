using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpawnersInformationChanger))]
[RequireComponent(typeof(ZombiesBooster))]
public class ZombiesSpawner : MonoBehaviour
{
    [SerializeField] private List<Zombie> _zombies;
    [SerializeField] private int _startCountZombies;
    [SerializeField] private int _leftmostPosition;
    [SerializeField] private int _rightmostPosition;
    [SerializeField] private Transform _pool;
    [SerializeField] private Base _target;
    [SerializeField] private SurvivorsShopOpportunities _survivor;
    [SerializeField] private Button _button;

    private int _currentListCapasity;
    private int _stepZombiesValueIncrease;
    private int _currentCountZombies;
    private List<Zombie> _currentWave;
    private Coroutine _zombiesSpawnCoroutine;
    private SpawnersInformationChanger _informator;
    private ZombiesBooster _zombiesBooster;

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
        _informator = GetComponent<SpawnersInformationChanger>();
        _zombiesBooster = GetComponent<ZombiesBooster>();
        _currentListCapasity = 1;
        _currentCountZombies = _startCountZombies;

        CreateNewWave();
    }

    private void Update()
    {
        if (CheckAliveZombies())
            Restart();
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

        _button.gameObject.SetActive(false);
        tempZombiesList = CreateTempList(_zombies);
        _currentWave = new List<Zombie>();

        _informator.ChangeWaveInformation();
        _informator.ShowCurrentWaveLabel();

        if (_pool.childCount > 0)
            ClearPool();

        for (int i = 0; i < _currentListCapasity; i++)
        {
            tempZombie = tempZombiesList[Random.Range(0, tempZombiesList.Count)];
            tempZombiesList.Remove(tempZombie);
            _currentWave.Add(tempZombie);
        }

        if (_informator.WaveIndex % 2 != 0 && _informator.WaveIndex != 1)
            _zombiesBooster.IncreaseMultiplier();

        if (_currentListCapasity < _zombies.Count)
            _currentListCapasity++;

        _zombiesSpawnCoroutine = StartCoroutine(SpawnZombies());
    }

    private void Restart()
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
            tempZombie.IncreaseHealth(_zombiesBooster.MultiplierHealth);
            yield return timer;
        }

        _informator.IncreaseWaveIndex();
        _stepZombiesValueIncrease = Random.Range(3, 8);
        _currentCountZombies += _stepZombiesValueIncrease;
        StopCoroutine(_zombiesSpawnCoroutine);
    }
}
