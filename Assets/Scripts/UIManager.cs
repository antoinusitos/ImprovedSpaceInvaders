using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager GetInstance()
    {
        return _instance;
    }

    public Text scoreText;
    public Text livesText;
    public Text waveText;

    private ScoreManager _scoreManager;
    private FormationManager _formationManager;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _scoreManager = ScoreManager.GetInstance();
        _formationManager = FormationManager.GetInstance();
    }

    private void Update()
    {
        scoreText.text = "Score : " + _scoreManager.GetScore();
        waveText.text = "Wave " + _formationManager.GetCurrentWave() + "/" + _formationManager.GetTotalWaves();
    }
}
