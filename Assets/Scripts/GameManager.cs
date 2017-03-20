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

    void Start()
    {
        _uiManager = UIManager.GetInstance();
    }

    public void Loose()
    {

    }

    public void Win()
    {

    }
}
