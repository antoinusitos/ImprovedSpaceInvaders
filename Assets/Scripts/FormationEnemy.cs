using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationEnemy : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    public GameObject enemyPrefab;

    private List<GameObject> _children;

    public int _numberOfEnemy = 12;
    public float multiplierdistance = 2f;

    private int _formationLevel = 1;
    private float _angleCumulate = 0.0f;

    void Start ()
    {
        _children = new List<GameObject>();
        for(int i = 0; i < _numberOfEnemy; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistance;
            float y = Mathf.Sin(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistance;
            GameObject aEnemy = Instantiate(enemyPrefab, new Vector3(x, 0.55f, y), Quaternion.identity);
            aEnemy.transform.LookAt(new Vector3(0, aEnemy.transform.position.y, 0));
            aEnemy.transform.parent = transform;
            _children.Add(aEnemy);
        }
    }
	
	void Update ()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        _angleCumulate += rotationSpeed * Time.deltaTime;
        if (_angleCumulate >= 360.0f * _formationLevel)
        {
            _formationLevel++;
            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i])
                {
                    float x = Mathf.Cos(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistance * _formationLevel;
                    float y = Mathf.Sin(Mathf.Deg2Rad * (360.0f / _numberOfEnemy) * i) * multiplierdistance * _formationLevel;

                    _children[i].transform.position = new Vector3(x, 0.55f, y);
                    _children[i].transform.LookAt(new Vector3(0, _children[i].transform.position.y, 0));
                }
            }
        }

        if(CheckEmptyList())
        {
            SoundManager.GetInstance().playSound(SoundManager.soundToPlay.waveClear);
            Destroy(this);
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
