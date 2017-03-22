using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float rotationSpeed = 5.0f;

    private bool _canMove = true;

    void Update()
    {
        if(_canMove)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetCanMove(bool newState)
    {
        _canMove = newState;
    }

}
