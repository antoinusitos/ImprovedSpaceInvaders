using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMaxScore : MonoBehaviour
{
	void Start ()
    {
        int scoreMax = 0;
        scoreMax = PlayerPrefs.GetInt("MaxScore");
        GetComponent<Text>().text = "Max Score : " + scoreMax;
    }

}
