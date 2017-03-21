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
    private ScoreManager _scoreManager;

    private bool _isWaiting = true;
    
    public PlayerShoot playerShoot;
    public PlayerLife playerLife;
    public PlayerMovement playerMovement;

    public PlayerShoot playerShootP2;
    public PlayerLife playerLifeP2;
    public PlayerMovement playerMovementP2;

    public GameObject protectionsPrefab;
    private GameObject _protectionsInstance;

    int nbPlayers = 1;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _uiManager = UIManager.GetInstance();
        _formationManager = FormationManager.GetInstance();
        _inputManager = InputManager.GetInstance();
        _scoreManager = ScoreManager.GetInstance();
        StartMenu();
    }

    public void Lose()
    {
        _uiManager.ShowStartText(false);
        _uiManager.ShowWinText(false);
        _uiManager.ShowLoseText(true);
        _formationManager.StopAllFormations();
        BlockPlayer();
        RemoveAllBullets();
        _scoreManager.SaveMaxScore();
        StartCoroutine(Restart());
    }

    void RemoveAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
        bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
    }

    void BlockPlayer()
    {
        playerMovement.SetCanMove(false);
        playerMovementP2.SetCanMove(false);
        playerShoot.SetMustStop(true);
        playerShootP2.SetMustStop(true);
    }

    public void Win()
    {
        _uiManager.ShowStartText(false);
        _uiManager.ShowWinText(true);
        _uiManager.ShowLoseText(false);
        _formationManager.StopAllFormations();
        BlockPlayer();
        RemoveAllBullets();
        _scoreManager.SaveMaxScore();
        StartCoroutine(Restart());
    }

    public void StartMenu()
    {
        _formationManager.RemoveAllFormations();
        _uiManager.ShowStartText(true);
        _uiManager.ShowWinText(false);
        _uiManager.ShowLoseText(false);
        _isWaiting = true;
    }

    private void Reset()
    {
        _formationManager.RemoveAllFormations();
        playerShoot.Reset();
        playerShootP2.Reset();
        playerLife.Reset();
        playerLifeP2.Reset();
        _scoreManager.Reset();
        if(_protectionsInstance)
            Destroy(_protectionsInstance);
    }

    public void StartGame()
    {
        Reset();
        _protectionsInstance = Instantiate(protectionsPrefab);
        _uiManager.ShowStartText(false);
        _uiManager.ShowWinText(false);
        _uiManager.ShowLoseText(false);
        _isWaiting = false;
        _uiManager.ShowWaveText(true);
        _formationManager.StartGame();
        playerMovement.SetCanMove(true);
        playerMovementP2.SetCanMove(true);
        playerShoot.SetMustStop(false);
        playerShootP2.SetMustStop(false);

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

        if(nbPlayers == 1 && _inputManager.AButtonPressedP2())
        {
            nbPlayers = 2;
            _uiManager.HidePlayer2JoinText();
            playerMovementP2.gameObject.SetActive(true);
            _uiManager.ActivatePlayer2UI();
        }
    }
}
