using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        return _instance;
    }

    private UIManager _uiManager;
    private FormationManager _formationManager;
    private InputManager _inputManager;

    private bool _isWaiting = true;

    void Start()
    {
        _uiManager = UIManager.GetInstance();
        _formationManager = FormationManager.GetInstance();
        _inputManager = InputManager.GetInstance();
        StartMenu();
    }

    public void Lose()
    {
        _uiManager.ShowStartText(false);
        _uiManager.ShowWinText(false);
        _uiManager.ShowLoseText(true);
    }

    public void Win()
    {
        _uiManager.ShowStartText(false);
        _uiManager.ShowWinText(true);
        _uiManager.ShowLoseText(false);
    }

    public void StartMenu()
    {
        _uiManager.ShowStartText(true);
        _uiManager.ShowWinText(false);
        _uiManager.ShowLoseText(false);
        _isWaiting = true;
    }

    public void StartGame()
    {
        _uiManager.ShowStartText(false);
        _uiManager.ShowWinText(false);
        _uiManager.ShowLoseText(false);
        _isWaiting = false;
        _uiManager.ShowWaveText(true);
        _formationManager.StartGame();
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3.0f);
        StartMenu();
    }

    private void Update()
    {
        if(_inputManager.AButtonPressed() && _isWaiting)
        {
            StartGame();
        }
    }
}
