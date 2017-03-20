using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    public bool isQuit = false;

    public void Execute()
    {
        if(isQuit)
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(1);
        }
    }

}
