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
    private bool direction = true;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        SpawnNewWave();
    }

    void SpawnNewWave()
    {
        _currentWave++;
        GameObject aFormation = Instantiate(formationPrefab);
        if (!direction)
            aFormation.GetComponent<FormationEnemy>().ChangeDirection();
        direction = !direction;
    }

    public int GetTotalWaves()
    {
        return totalWaves;
    }

    public int GetCurrentWave()
    {
        return _currentWave;
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
    }
	
}
