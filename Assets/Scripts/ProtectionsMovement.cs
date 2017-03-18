using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionsMovement : MonoBehaviour
{
    public float movementSpeed = 2.0f;

    public float timeToRotateMax = 8.0f;
    public float timeToRotateMin = 4.0f;

    public float timeTostop = 1.0f;
    private float _currentTimeTostop = 0.0f;

    private bool _direction = true;
    private float _timeToRotate = 6.0f;
    private float _currentTimeToRotate = 0.0f;

    private bool _isMoving = false;

	void Update ()
    {
        if (_isMoving)
        {
            transform.Rotate(Vector3.up, movementSpeed * Time.deltaTime);

            _currentTimeToRotate += Time.deltaTime;
            if (_currentTimeToRotate >= _timeToRotate)
            {
                _currentTimeToRotate = 0.0f;
                _timeToRotate = Random.Range(timeToRotateMin, timeToRotateMax);
                ChangeDirection();
                _isMoving = false;
            }
        }
        else
        {
            _currentTimeTostop += Time.deltaTime;
            if(_currentTimeTostop >= timeTostop)
            {
                _currentTimeTostop = 0.0f;
                _isMoving = true;
            }
        }
    }

    void ChangeDirection()
    {
        movementSpeed *= -1.0f;
    }
}
