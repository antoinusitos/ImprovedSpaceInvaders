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

    private float _tempOffsetMax = 1.0f;
    private float _tempOffsetMin = -1.0f;
    private float _tempShakeTime = 0.2f;
    private float _tempShakeSpeed = 5.0f;
    private bool _usingTemp = false;

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

    public void Shake(float theOffsetMax, float theOffsetMin, float theshakeTime, float theShakeSpeed)
    {
        _shaking = true;
        _currentShakeTime = 0.0f;
        _usingTemp = true;
        _tempOffsetMax = theOffsetMax;
        _tempOffsetMin = theOffsetMin;
        _tempShakeTime = theshakeTime;
        _tempShakeSpeed = theShakeSpeed;
    }

    private void Update()
    {
        if(_shaking)
        {
            if (!_usingTemp)
            {
                _shakePos = new Vector3(Random.Range(screenOffsetMin, screenOffsetMax), _startPos.y, Random.Range(screenOffsetMin, screenOffsetMax));
                transform.position = Vector3.Lerp(transform.position, _shakePos, shakeSpeed * Time.deltaTime);

                _currentShakeTime += Time.deltaTime;
                if (_currentShakeTime >= shakeTime)
                {
                    _shaking = false;
                    transform.position = Vector3.Lerp(transform.position, _startPos, shakeSpeed * Time.deltaTime);
                }
            }
            else
            {
                _shakePos = new Vector3(Random.Range(_tempOffsetMin, _tempOffsetMax), _startPos.y, Random.Range(_tempOffsetMin, _tempOffsetMax));
                transform.position = Vector3.Lerp(transform.position, _shakePos, _tempShakeSpeed * Time.deltaTime);

                _currentShakeTime += Time.deltaTime;
                if (_currentShakeTime >= _tempShakeTime)
                {
                    _shaking = false;
                    _usingTemp = false;
                    transform.position = Vector3.Lerp(transform.position, _startPos, _tempShakeSpeed * Time.deltaTime);
                }
            }
        }
    }
}
