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
    public int totalWaves = 3;
    private int _currentWave = 1;
    public int totalFormations = 4;
    private int _currentFormation = 0;
    private List<GameObject> _allWaves;
    private bool _canSpawn = false;

    private bool direction = true;

    public float timeBetweenGames = 5.0f;
    private float _currentTimeBetweenGames = 0.0f;
    private bool _waitingForNextGame = false;
    private bool _iswaiting = false;


    private SoundManager _soundManager;
    private UIManager _uiManager;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _soundManager = SoundManager.GetInstance();
        _uiManager = UIManager.GetInstance();
        _allWaves = new List<GameObject>();
    }

    public void StartGame()
    {
        StartCoroutine("SpawnTime");
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(timeBetweenGames);
        _uiManager.ShowWaveText(false);
        _canSpawn = true;
        SpawnNewWave();
    }

    void SpawnNewWave()
    {
        _currentFormation++;
        _soundManager.playSound(SoundManager.soundToPlay.spawn);
        GameObject aFormation = Instantiate(formationPrefab);
        if (!direction)
            aFormation.GetComponent<FormationEnemy>().ChangeDirection();
        direction = !direction;
        _allWaves.Add(aFormation);
        if (_allWaves.Count == 1)
        {
            aFormation.GetComponent<FormationEnemy>().SetAllowToShoot(true);
        }
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
        else
        {
            _allWaves[0].GetComponent<FormationEnemy>().AllowTheWavetoShoot();
        }
    }

    void Update()
    {
        if (_currentFormation < totalFormations && _canSpawn)
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
            if (!_iswaiting)
            {
                _currentFormation = 0;
                _iswaiting = true;
                _currentWave++;
                _uiManager.ShowWaveText(true);
            }
            _currentTimeBetweenGames += Time.deltaTime;
            if (_currentTimeBetweenGames >= timeBetweenGames)
            {
                _waitingForNextGame = false;
                _currentTimeBetweenGames = 0.0f;
                _currentWave = 0;
                _uiManager.ShowWaveText(false);
                _iswaiting = false;
                SpawnNewWave();
            }
        }
    }
}
