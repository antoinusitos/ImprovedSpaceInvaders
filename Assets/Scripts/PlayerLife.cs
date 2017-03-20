﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life = 3;

    private UIManager _uiManager;
    private GameManager _gameManager;

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
            _uiManager.UpdatePlayerLife(life);
            if (life <= 0)
            {
                _gameManager.Lose();
            }
        }
    }
}
