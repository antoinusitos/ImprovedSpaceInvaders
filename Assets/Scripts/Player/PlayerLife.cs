using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 3;

    public UIManager _uiManager;
    private GameManager _gameManager;
    private InputManager _inputManager;

    public int indexPlayer = 0;

    void Start()
    {
        _uiManager = UIManager.GetInstance();
        _gameManager = GameManager.GetInstance();
        _inputManager = InputManager.GetInstance();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Bullet bullet = collider.transform.GetComponent<Bullet>();
        if (bullet && collider.transform.tag == "EnemyBullet")
        {
            Destroy(collider.gameObject);
            life--;
            _inputManager.Vibration(indexPlayer);
            if (indexPlayer == 0)
                _uiManager.UpdatePlayerLife(life);
            else
                _uiManager.UpdatePlayer2Life(life);
            if (life <= 0)
            {
                _gameManager.Lose();
            }
        }
    }

    public void Reset()
    {
        life = 3;
        if (indexPlayer == 0)
            _uiManager.UpdatePlayerLife(life);
        else
            _uiManager.UpdatePlayer2Life(life);
    }

    public void RefillLife()
    {
        life++;
        if (_uiManager)
        {
            if (indexPlayer == 0)
                _uiManager.UpdatePlayerLife(life);
            else
                _uiManager.UpdatePlayer2Life(life);
        }
    }
}
