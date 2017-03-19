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
    public Text lifeText;
    public Text waveText;
    public Slider lifeSlider;
    public Slider powerSlider;
    public GameObject powerImage;

    private ScoreManager _scoreManager;
    private FormationManager _formationManager;
    private int _life = 100;

    private int _oldLife = 100;
    private bool _changeSliderPos = false;
    private float _currentWait = 0.0f;
    public float changeLifespeed = 0.05f;

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

    public void UpdatePowerSlider(float rate)
    {
        powerSlider.value = rate;
        if(rate == 1)
        {
            powerImage.SetActive(true);
        }
        else if (rate == 0)
        {
            powerImage.SetActive(false);
        }
    }

    public void UpdateLife(int Amount)
    {
        _oldLife = _life;
        _life += Amount;
        _changeSliderPos = true;
    }

    private void Update()
    {
        scoreText.text = "Score : " + _scoreManager.GetScore();
        waveText.text = "Wave " + _formationManager.GetCurrentWave() + "/" + _formationManager.GetTotalWaves();

        if(_changeSliderPos)
        {
            _currentWait += Time.deltaTime;
            if (_currentWait >= changeLifespeed)
            {
                _currentWait = 0.0f;
                if (_oldLife < _life)
                {
                    _oldLife += 1;
                }
                else
                {
                    _oldLife -= 1;
                }
                if(_oldLife == _life)
                {
                    _changeSliderPos = false;
                }
                lifeSlider.value = _oldLife;
                lifeText.text = _oldLife + "%";
            }
        }
    }
}
