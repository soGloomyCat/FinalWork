using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    private int _currentWaveIndex;
    private int _currentListCapasity;
    private int _stepZombiesValueIncrease;
    private int _currentCountZombies;
    private List<Zombie> _currentWave;
    private Coroutine _zombiesSpawnCoroutine;
    private ZombiesBooster _zombiesBooster;
    private SpawnerView _spawnerView;

    public event UnityAction BoostZombies;
    public event UnityAction ChangedInfo;
    public event UnityAction ShowInfo;
    public event UnityAction WaveIndexIncreased;
    public event UnityAction<int> ZombieKilled;
    public event UnityAction WaveFinished;

    private void OnEnable()
    {
        _spawnerView = GetComponent<SpawnerView>();
        _spawnerView.ButtonPressed += CreateNewWave;
    }

    private void OnDisable()
    {
        _spawnerView.ButtonPressed -= CreateNewWave;
    }

    private void Start()
    {
        _currentWaveIndex = 1;
        _currentListCapasity = 1;
        _currentCountZombies = _startCountZombies;
        _zombiesBooster = GetComponent<ZombiesBooster>();

        CreateNewWave();
    }

    private void Update()
    {
        if (CheckAliveZombies())
            RestartWave();
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

        tempZombiesList = CreateTempList(_zombies);
        _currentWave = new List<Zombie>();

        ChangedInfo?.Invoke();
        ShowInfo?.Invoke();

        if (_pool.childCount > 0)
            ClearPool();

        for (int i = 0; i < _currentListCapasity; i++)
        {
            tempZombie = tempZombiesList[Random.Range(0, tempZombiesList.Count)];
            tempZombiesList.Remove(tempZombie);
            _currentWave.Add(tempZombie);
        }

        if (_currentWaveIndex % 2 != 0 && _currentWaveIndex != 1)
            BoostZombies?.Invoke();

        if (_currentListCapasity < _zombies.Count)
            _currentListCapasity++;

        _currentWaveIndex++;
        _zombiesSpawnCoroutine = StartCoroutine(SpawnZombies());
    }

    private void RestartWave()
    {
        WaveFinished?.Invoke();
    }

    private void OnZombieDying(Zombie zombie)
    {
        zombie.Dying -= OnZombieDying;
        ZombieKilled?.Invoke(zombie.Reward);
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
            tempZombie.InizializeParameters(_target, _zombiesBooster.MultiplierHealth);
            yield return timer;
        }

        WaveIndexIncreased?.Invoke();
        _stepZombiesValueIncrease = Random.Range(3, 8);
        _currentCountZombies += _stepZombiesValueIncrease;
        StopCoroutine(_zombiesSpawnCoroutine);
    }
}
