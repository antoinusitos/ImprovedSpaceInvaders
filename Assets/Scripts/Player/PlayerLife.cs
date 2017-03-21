using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 3;

    private UIManager _uiManager;
    private GameManager _gameManager;

    public int indexPlayer = 0;

    void Start()
    {
        _uiManager = UIManager.GetInstance();
        _gameManager = GameManager.GetInstance();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Bullet bullet = collider.transform.GetComponent<Bullet>();
        if (bullet && collider.transform.tag == "EnemyBullet")
        {
            Destroy(collider.gameObject);
            life--;
            if(indexPlayer == 0)
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
        if (indexPlayer == 0)
            _uiManager.UpdatePlayerLife(life);
        else
            _uiManager.UpdatePlayer2Life(life);
    }
}
