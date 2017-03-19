using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationManager : MonoBehaviour
{
    private static FormationManager _instance;

    public static FormationManager GetInstance()
    {
        return _instance;
    }

    public GameObject formationPrefab;
    public float timeBetweenWaves = 20.0f;
    private float _currentReload = 0.0f;
    public int totalWaves = 4;
    private int _currentWave = 0;
    private List<GameObject> _allWaves;

    private bool direction = true;

    public float timeBetweenGames = 5.0f;
    private float _currentTimeBetweenGames = 0.0f;
    private bool _waitingForNextGame = false;

    private SoundManager _soundManager;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _soundManager = SoundManager.GetInstance();
        _allWaves = new List<GameObject>();
        StartCoroutine("SpawnTime");
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(_currentTimeBetweenGames);
        SpawnNewWave();
    }

    void SpawnNewWave()
    {
        _soundManager.playSound(SoundManager.soundToPlay.spawn);
        _currentWave++;
        GameObject aFormation = Instantiate(formationPrefab);
        if (!direction)
            aFormation.GetComponent<FormationEnemy>().ChangeDirection();
        direction = !direction;
        _allWaves.Add(aFormation);
    }

    public int GetTotalWaves()
    {
        return totalWaves;
    }

    public int GetCurrentWave()
    {
        return _currentWave;
    }

    public void DestroyAWave(GameObject theWave)
    {
        _allWaves.Remove(theWave);
        if (_allWaves.Count == 0)
        {
            _waitingForNextGame = true;
        }
    }

    void Update()
    {
        if (_currentWave < totalWaves)
        {
            _currentReload += Time.deltaTime;
            if (_currentReload >= timeBetweenWaves)
            {
                _currentReload = 0.0f;
                SpawnNewWave();
            }
        }
        else if(_waitingForNextGame)
        {
            _currentTimeBetweenGames += Time.deltaTime;
            if (_currentTimeBetweenGames >= timeBetweenGames)
            {
                _waitingForNextGame = false;
                _currentTimeBetweenGames = 0.0f;
                _currentWave = 0;
                SpawnNewWave();
            }
        }
    }
}
