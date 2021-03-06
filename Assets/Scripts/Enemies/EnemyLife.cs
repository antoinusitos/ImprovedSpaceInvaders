﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int scoreEarn = 100;

    public int life = 5;

    public int lifeRemoved = 1;

    private ScreenShakeManager _screenShakeManager;
    private ScoreManager _scoreManager;
    private GameManager _gameManager;

    public bool isBoss = false;

    private void Start()
    {
        _screenShakeManager = ScreenShakeManager.GetInstance();
        _scoreManager = ScoreManager.GetInstance();
        _gameManager = GameManager.GetInstance();
    }

    private void TakeDamage(int Amount, PlayerBullet thePlayerBullet)
    {
        life -= Amount;

        if(life <= 0)
        {
            SoundManager.GetInstance().playSound(SoundManager.soundToPlay.explosion);
            _screenShakeManager.Shake();
            _scoreManager.AddScore(scoreEarn);
            if (!isBoss)
            {
                if (thePlayerBullet.GetOwner())
                    thePlayerBullet.GetOwner().gameObject.GetComponent<PlayerShoot>().AddReloadSpecialBullet();
                GetComponent<EnemyShoot>().Shoot();
            }
            else
            {
                _gameManager.Win();
            }
            Destroy(gameObject);
        }
    }
	
    private void OnTriggerEnter(Collider collider)
    {
        PlayerBullet playerBullet = collider.transform.GetComponent<PlayerBullet>();
        if (playerBullet)
        {
            TakeDamage(playerBullet.GetDamage(), playerBullet);
            Destroy(collider.gameObject);
        }
    }
}
