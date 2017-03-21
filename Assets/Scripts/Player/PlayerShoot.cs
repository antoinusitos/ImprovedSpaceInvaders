using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.2f;
    private float _currentReload = 0.0f;
    private bool _canShoot = true;

    private bool _superPowerActive = false;

    public GameObject bulletPrefab;
    public GameObject specialBulletPrefab;
    public GameObject bulletSpawn;
    public GameObject superBulletSpawn;
    public GameObject superBulletSpawn2;

    private int _nbSuperBullet = 6;
    private int _currentNbSuperBullet = 0;
    private int _currentNbBeforeSpecialBullet = 0;
    public int nbBeforeSpecialBullet = 10;
    private float _reloadSpecialBullet = 0.0f;
    private float _SpecialBulletFireRate = 0.1f;
    private bool _spawnOne = true;
    private bool _shootingSuperBullet = false;

    public PlayerMovement playerMovement;

    private bool _mustStop = false;

    public int indexPlayer = 0;

    //accessor
    private InputManager _inputManager;
    private SoundManager _soundManager;
    private UIManager _uiManager;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
        _soundManager = SoundManager.GetInstance();
        _uiManager = UIManager.GetInstance();
    }

    public void AddReloadSpecialBullet()
    {
        _currentNbBeforeSpecialBullet++;
        if (indexPlayer == 0)
            _uiManager.UpdatePowerSlider((float)_currentNbBeforeSpecialBullet / (float)nbBeforeSpecialBullet);
        else
            _uiManager.UpdatePowerSliderP2((float)_currentNbBeforeSpecialBullet / (float)nbBeforeSpecialBullet);
        if (_currentNbBeforeSpecialBullet >= nbBeforeSpecialBullet && !_superPowerActive)
        {
            if (indexPlayer == 0)
                _uiManager.UpdatePowerSlider(1);
            else
                _uiManager.UpdatePowerSliderP2(1);
            _soundManager.playSound(SoundManager.soundToPlay.superPower);
            _superPowerActive = true;
        }
    }

    public void Reset()
    {
        _superPowerActive = false;
        _currentNbBeforeSpecialBullet = 0;
        if (indexPlayer == 0)
            _uiManager.UpdatePowerSlider((float)_currentNbBeforeSpecialBullet / (float)nbBeforeSpecialBullet);
        else
            _uiManager.UpdatePowerSliderP2((float)_currentNbBeforeSpecialBullet / (float)nbBeforeSpecialBullet);
    }

    private void Update()
    {
        if (_mustStop) return;

        if(!_canShoot)
        {
            _currentReload += Time.deltaTime;
            if(_currentReload >= fireRate)
            {
                _currentReload = 0.0f;
                _canShoot = true;
            }
        }

        if(_canShoot && ((indexPlayer == 0 && _inputManager.RightTriggerPressed()) || (indexPlayer == 1 && _inputManager.RightTriggerPressedP2())))
        {
            _canShoot = false;
            if (bulletPrefab)
            {
                GameObject aBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
                Vector2 dir = new Vector2(-transform.position.x, -transform.position.z);
                aBullet.GetComponent<Bullet>().SetDirection(dir);
                aBullet.GetComponent<PlayerBullet>().SetOwner(gameObject);
            }
            else
                Debug.LogError("No bulletPrefab on " + name);
        }

        if(_superPowerActive && ((indexPlayer == 0 && _inputManager.SuperPowerButtonPressed()) || (indexPlayer == 1 && _inputManager.SuperPowerButtonPressedP2())))
        {
            UseSuperPower();
            _superPowerActive = false;
        }

        if(_shootingSuperBullet)
        {
            _reloadSpecialBullet += Time.deltaTime;
            if(_reloadSpecialBullet >= _SpecialBulletFireRate)
            {
                _reloadSpecialBullet = 0.0f;
                if(_spawnOne)
                {
                    GameObject aBullet = Instantiate(specialBulletPrefab, superBulletSpawn.transform.position, superBulletSpawn.transform.localRotation);
                    aBullet.transform.rotation = Quaternion.Euler(0, playerMovement.GetAngle(), 0);
                    Vector2 dir = new Vector2(-transform.position.x, -transform.position.z);
                    aBullet.GetComponent<Bullet>().SetDirection(dir);
                }
                else
                {
                    GameObject aBullet = Instantiate(specialBulletPrefab, superBulletSpawn2.transform.position, superBulletSpawn2.transform.localRotation);
                    aBullet.transform.rotation = Quaternion.Euler(0, playerMovement.GetAngle(), 0);
                    Vector2 dir = new Vector2(-transform.position.x, -transform.position.z);
                    aBullet.GetComponent<Bullet>().SetDirection(dir);
                }

                _spawnOne = !_spawnOne;

                _currentNbSuperBullet++;
                if(_currentNbSuperBullet >= _nbSuperBullet)
                {
                    _currentNbSuperBullet = 0;
                    _shootingSuperBullet = false;
                }
            }
        }
    }

    void UseSuperPower()
    {
        _currentNbBeforeSpecialBullet = 0;
        if(indexPlayer == 0)
            _uiManager.UpdatePowerSlider(0);
        else
            _uiManager.UpdatePowerSliderP2(0);
        _shootingSuperBullet = true;
        GameObject aBullet = Instantiate(specialBulletPrefab, superBulletSpawn.transform.position, Quaternion.identity);
        Vector2 dir = new Vector2(-transform.position.x, -transform.position.z);
        aBullet.GetComponent<Bullet>().SetDirection(dir);
    }

    public void SetMustStop(bool newState)
    {
        _mustStop = newState;
    }
}
