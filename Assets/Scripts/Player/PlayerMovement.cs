using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float stickDeadZone = 0.2f;
    public float movementSpeed = 4.0f;

    private float _angle = 0.0f;

    private bool _canMove = false;

    public int indexPlayer = 0;

    //accessor
    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
    }

    private void Update()
    {
        if (_canMove)
        {
            GetStickOrientation();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, _angle, 0), Time.deltaTime * movementSpeed);
        }
    }

    public void SetCanMove(bool newState)
    {
        _canMove = newState;
    }

    public float GetAngle()
    {
        return _angle;
    }

    private void GetStickOrientation()
    {
        float x = 0; _inputManager.GetStickPosX();
        float z = 0; _inputManager.GetStickPosY();

        if(indexPlayer == 0)
        {
            x = _inputManager.GetStickPosX();
            z = _inputManager.GetStickPosY();
        }
        else
        {
            x = _inputManager.GetStickPosXP2();
            z = _inputManager.GetStickPosYP2();
        }

        Vector3 v = Vector3.zero;

        if (x > stickDeadZone || x < -stickDeadZone)
        {
            v.x = x;
        }

        if (z > stickDeadZone || z < -stickDeadZone)
        {
            v.z = z;
        }

        if (v != Vector3.zero)
        {

            v = v.normalized;

            _angle = Mathf.Atan2(v.z, -v.x) * 180 / Mathf.PI - 90.0f;
        }

    }
}
