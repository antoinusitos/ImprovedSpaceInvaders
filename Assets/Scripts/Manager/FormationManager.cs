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
    public float timeBetweenFormation = 10.0f;
    private float _currentReload = 0.0f;

    public int totalWaves = 4;
    private int _currentWave = 1;

    public int totalFormations = 4;
    private int _currentFormation = 0;
    private List<GameObject> _allFormations;
    private bool _canSpawn = false;

    private bool direction = true;

    public float timeBetweenWaves = 5.0f;
    private float _currentTimeBetweenWaves = 0.0f;
    private bool _waitingForNextWave = false;
    private bool _iswaiting = false;

    public GameObject player;
    public GameObject player2;

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
        _allFormations = new List<GameObject>();
    }

    public void StartGame()
    {
        StartCoroutine("SpawnTime");
    }

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(3.0f);
        _uiManager.ShowWaveText(false);
        _canSpawn = true;
        SpawnNewFormation();
    }

    public void StopAllFormations()
    {
        _canSpawn = false;
        for (int i = 0; i < _allFormations.Count; i++)
        {
            _allFormations[i].GetComponent<FormationEnemy>().StopFormation();
        }
    }

    public void RemoveAllFormations()
    {
        if (_allFormations != null)
        {
            for (int i = 0; i < _allFormations.Count; i++)
            {
                _allFormations[i].GetComponent<FormationEnemy>().RemoveAllEnemies();
            }
        }
    }

    void SpawnNewFormation()
    {
        _currentFormation++;
        _soundManager.playSound(SoundManager.soundToPlay.spawn);
        GameObject aFormation = Instantiate(formationPrefab);
        if (!direction)
            aFormation.GetComponent<FormationEnemy>().ChangeDirection();
        aFormation.GetComponent<FormationEnemy>().ChangeLevel(_currentWave);
        direction = !direction;
        _allFormations.Add(aFormation);
        if (_allFormations.Count == 1)
        {
            aFormation.GetComponent<FormationEnemy>().SetAllowToShoot(true);
        }
    }

    void SpawnBoss()
    {
        Debug.Log("spawn Boss");
    }

    public int GetTotalWaves()
    {
        return totalWaves;
    }

    public int GetCurrentWave()
    {
        return _currentWave;
    }

    public void DestroyAFormation(GameObject theFormation)
    {
        _allFormations.Remove(theFormation);
        if (_allFormations.Count == 0 && _currentFormation >= totalFormations)
        {
            _waitingForNextWave = true;
        }
        else if(_allFormations.Count > 0 && _allFormations[0] != null)
        {
            _allFormations[0].GetComponent<FormationEnemy>().AllowTheFormationtoShoot();
        }
    }

    void Update()
    {
        if (_currentFormation < totalFormations && _canSpawn)
        {
            _currentReload += Time.deltaTime;
            if (_currentReload >= timeBetweenFormation)
            {
                _currentReload = 0.0f;
                SpawnNewFormation();
            }
        }
        else if(_waitingForNextWave && _canSpawn)
        {
            if (!_iswaiting)
            {
                _iswaiting = true;
                _currentWave++;
                _uiManager.ShowWaveText(true);
                player.GetComponent<PlayerLife>().RefillLife();
                player2.GetComponent<PlayerLife>().RefillLife();
            }
            _currentTimeBetweenWaves += Time.deltaTime;
            if (_currentTimeBetweenWaves >= timeBetweenWaves)
            {
                _iswaiting = false;
                _currentFormation = 0;
                _waitingForNextWave = false;
                _currentTimeBetweenWaves = 0.0f;
                _uiManager.ShowWaveText(false);
                if(_currentWave == totalWaves)
                {
                    Debug.Log(_currentWave);
                    SpawnBoss();
                }
                else
                {
                    SpawnNewFormation();
                }
            }
        }
    }
}
