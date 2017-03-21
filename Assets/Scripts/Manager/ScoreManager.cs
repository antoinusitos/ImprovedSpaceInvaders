using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public static ScoreManager GetInstance()
    {
        return _instance;
    }

    private int _score = 0;

    void Awake()
    {
        _instance = this;
    }

    public void AddScore(int Amount)
    {
        _score += Amount;
    }

    public int GetScore()
    {
        return _score;
    }

    public void Reset()
    {
        _score = 0;
    }

    public void SaveMaxScore()
    {
        int maxScore = 0;
        maxScore = PlayerPrefs.GetInt("MaxScore");
        if(_score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", _score);
        }
    }
}
