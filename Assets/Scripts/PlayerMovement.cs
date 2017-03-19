using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float stickDeadZone = 0.2f;
    public float movementSpeed = 4.0f;

    private float _angle = 0.0f;

    //accessor
    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
    }

    private void Update()
    {
        GetStickOrientation();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, _angle, 0), Time.deltaTime * movementSpeed);
    }

    public float GetAngle()
    {
        return _angle;
    }

    private void GetStickOrientation()
    {
        float x = _inputManager.GetStickPosX();
        float z = _inputManager.GetStickPosY();

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
