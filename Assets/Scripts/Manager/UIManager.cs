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
    public Slider powerSliderP2;
    public GameObject powerImageP2;

    public GameObject playerLife1;
    public GameObject playerLife2;
    public GameObject playerLife3;

    public GameObject player2Life1;
    public GameObject player2Life2;
    public GameObject player2Life3;

    public GameObject winText;
    public GameObject loseText;
    public GameObject StartText;

    public GameObject player2JoinText;

    private ScoreManager _scoreManager;
    private FormationManager _formationManager;
    private int _life = 100;

    private int _oldLife = 100;
    private bool _changeSliderPos = false;
    private float _currentWait = 0.0f;
    public float changeLifeSpeed = 0.05f;

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

    public void Reset()
    {
        _life = 100;
    }

    public void HidePlayer2JoinText()
    {
        player2JoinText.SetActive(false);
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

    public void UpdatePowerSliderP2(float rate)
    {
        powerSliderP2.value = rate;
        if (rate == 1)
        {
            powerImageP2.SetActive(true);
        }
        else if (rate == 0)
        {
            powerImageP2.SetActive(false);
        }
    }

    public void UpdateLife(int Amount)
    {
        _oldLife = _life;
        _life += Amount;
        _changeSliderPos = true;
    }

    public void UpdatePlayerLife(int newAmount)
    {
        if(newAmount == 3)
        {
            playerLife1.SetActive(true);
            playerLife2.SetActive(true);
            playerLife3.SetActive(true);
        }
        else if (newAmount == 2)
        {
            playerLife1.SetActive(true);
            playerLife2.SetActive(true);
            playerLife3.SetActive(false);
        }
        else if (newAmount == 1)
        {
            playerLife1.SetActive(true);
            playerLife2.SetActive(false);
            playerLife3.SetActive(false);
        }
        else if (newAmount == 0)
        {
            playerLife1.SetActive(false);
            playerLife2.SetActive(false);
            playerLife3.SetActive(false);
        }
    }

    public void UpdatePlayer2Life(int newAmount)
    {
        if (newAmount == 3)
        {
            player2Life1.SetActive(true);
            player2Life2.SetActive(true);
            player2Life3.SetActive(true);
        }
        else if (newAmount == 2)
        {
            player2Life1.SetActive(true);
            player2Life2.SetActive(true);
            player2Life3.SetActive(false);
        }
        else if (newAmount == 1)
        {
            player2Life1.SetActive(true);
            player2Life2.SetActive(false);
            player2Life3.SetActive(false);
        }
        else if (newAmount == 0)
        {
            player2Life1.SetActive(false);
            player2Life2.SetActive(false);
            player2Life3.SetActive(false);
        }
    }

    public void ShowWaveText(bool newState)
    {
        waveText.gameObject.SetActive(newState);
    }

    public void ShowStartText(bool newState)
    {
        StartText.gameObject.SetActive(newState);
    }

    public void ShowWinText(bool newState)
    {
        winText.gameObject.SetActive(newState);
    }

    public void ShowLoseText(bool newState)
    {
        loseText.gameObject.SetActive(newState);
    }

    private void Update()
    {
        scoreText.text = "Score : " + _scoreManager.GetScore();
        waveText.text = "Wave " + _formationManager.GetCurrentWave() + "/" + _formationManager.GetTotalWaves();

        if(_changeSliderPos)
        {
            _currentWait += Time.deltaTime;
            if (_currentWait >= changeLifeSpeed)
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
