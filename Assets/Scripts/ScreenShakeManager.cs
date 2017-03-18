using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour
{
    private static ScreenShakeManager _instance;

    public static ScreenShakeManager GetInstance()
    {
        return _instance;
    }

    public float screenOffsetMax = 1.0f;
    public float screenOffsetMin = -1.0f;
    public float shakeTime = 0.2f;
    public float shakeSpeed = 5.0f;

    private bool _shaking = false;
    private float _currentShakeTime = 0.0f;
    private Vector3 _startPos = Vector3.zero;
    private Vector3 _shakePos = Vector3.zero;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    public void Shake()
    {
        _shaking = true;
        _currentShakeTime = 0.0f;
    }

    private void Update()
    {
        if(_shaking)
        {
            _shakePos = new Vector3(Random.Range(screenOffsetMin, screenOffsetMax), _startPos.y, Random.Range(screenOffsetMin, screenOffsetMax));
            transform.position = Vector3.Lerp(transform.position, _shakePos, shakeSpeed * Time.deltaTime);

            _currentShakeTime += Time.deltaTime;
            if(_currentShakeTime >= shakeTime)
            {
                _shaking = false;
                transform.position = Vector3.Lerp(transform.position, _startPos, shakeSpeed * Time.deltaTime);
            }
        }
    }
}
