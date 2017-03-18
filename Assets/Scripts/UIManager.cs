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
    private int _life = 100;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _scoreManager = ScoreManager.GetInstance();
        _formationManager = FormationManager.GetInstance();

        UpdateLife(0);
    }

    public void UpdateLife(int Amount)
    {
        _life += Amount;
        livesText.text = "Life : " + _life + "%";
    }

    private void Update()
    {
        scoreText.text = "Score : " + _scoreManager.GetScore();
        waveText.text = "Wave " + _formationManager.GetCurrentWave() + "/" + _formationManager.GetTotalWaves();
    }
}
