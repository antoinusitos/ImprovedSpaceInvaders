﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationEnemy : MonoBehaviour
{
    // formation rotation
    public float rotationSpeed = 5.0f;
    // speed of the lerp between levels
    public float speedMovementLevel = 2.0f;
    public GameObject enemyPrefab;

    private List<GameObject> _children;

    public int _numberOfEnemy = 12;
    // distance of the new wave compare to the old position
    public float multiplierdistance = 1.5f;
    // distance of the new wave at start
    public float multiplierdistanceBase = 2.0f;

    // after this level, the player take damages
    public int maxLevel = 5;
    // current level
    private int _formationLevel = 1;
    private float _angleCumulate = 0.0f;

    private bool _isMoving = true;

    private UIManager _uiManager;

    void Start ()
    {
        _uiManager = UIManager.GetInstance();

        _children = new List<GameObject>();
        for(int i = 0; i < _numberOfEnemy; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistanceBase;
            float y = Mathf.Sin(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistanceBase;
            GameObject aEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
            aEnemy.GetComponent<EnemyMovement>().SetDirection(new Vector3(x, 0.55f, y), speedMovementLevel);
            aEnemy.transform.LookAt(new Vector3(0, aEnemy.transform.position.y, 0));
            aEnemy.transform.parent = transform;
            _children.Add(aEnemy);
        }
        StartCoroutine(StartRotation());
    }

    public void ChangeDirection()
    {
        rotationSpeed *= -1;
    }

    void Update()
    {
        if (_isMoving)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            _angleCumulate += rotationSpeed * Time.deltaTime;
            if (_angleCumulate >= 360.0f * _formationLevel || _angleCumulate <= -360.0f * _formationLevel)
            {
                _formationLevel++;

                if (_formationLevel <= maxLevel)
                {
                    UpdateWave();
                    StartCoroutine(StartRotation());
                }
                else
                {
                    DestroyAll();
                    return;
               }
            }
        }
        if (CheckEmptyList())
        {
            SoundManager.GetInstance().playSound(SoundManager.soundToPlay.waveClear);
            Destroy(this);
        }
    }

    void UpdateWave()
    {
        for (int i = 0; i < _children.Count; i++)
        {
            if (_children[i])
            {
                float x = Mathf.Cos(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistance * _formationLevel;
                float y = Mathf.Sin(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistance * _formationLevel;

                _children[i].transform.LookAt(new Vector3(0, _children[i].transform.position.y, 0));
                _children[i].GetComponent<EnemyMovement>().SetDirection(new Vector3(x, 0.55f, y), speedMovementLevel);
                _isMoving = false;
            }
        }
    }

    void DestroyAll()
    {
        for (int i = 0; i < _children.Count; i++)
        {
            if (_children[i])
            {
                _uiManager.UpdateLife(-1);
                Destroy(_children[i].gameObject);
            }
        }
        Destroy(gameObject);
    }

    IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(speedMovementLevel);
        _isMoving = true;
        for (int i = 0; i < _children.Count; i++)
        {
            if (_children[i])
            {
                _children[i].GetComponent<EnemyMovement>().SetMoving(false);
            }
        }
    }

    bool CheckEmptyList()
    {
        for (int i = 0; i < _children.Count; i++)
        {
            if(_children[i])
            {
                return false;
            }
        }
        return true;
    }
}
