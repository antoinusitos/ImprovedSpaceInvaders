using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.2f;
    private float _currentReload = 0.0f;
    private bool _canShoot = true;

    private bool _superPowerActive = false;
    private bool _regenSuperPower = true;
    private float _timeToRegenSuperPower = 1.0f;
    private float _currentTimeToRegenSuperPower = 0.0f;

    public GameObject bulletPrefab;
    public GameObject specialBulletPrefab;
    public GameObject bulletSpawn;
    public GameObject superBulletSpawn;
    public GameObject superBulletSpawn2;

    private int _nbSuperBullet = 6;
    private int _currentNbSuperBullet = 0;
    private float _reloadSpecialBullet = 0.0f;
    private float _SpecialBulletFireRate = 0.1f;
    private bool _spawnOne = true;
    private bool _shootingSuperBullet = false;

    public PlayerMovement playerMovement;

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

    private void Update()
    {
        if(!_canShoot)
        {
            _currentReload += Time.deltaTime;
            if(_currentReload >= fireRate)
            {
                _currentReload = 0.0f;
                _canShoot = true;
            }
        }

        if(_canShoot && _inputManager.RightTriggerPressed())
        {
            _canShoot = false;
            if (bulletPrefab)
            {
                GameObject aBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
                Vector2 dir = new Vector2(-transform.position.x, -transform.position.z);
                aBullet.GetComponent<Bullet>().SetDirection(dir);
            }
            else
                Debug.LogError("No bulletPrefab on " + name);
        }
    
        if(_regenSuperPower)
        {
            _currentTimeToRegenSuperPower += Time.deltaTime;
            _uiManager.UpdatePowerSlider(_currentTimeToRegenSuperPower / _timeToRegenSuperPower);
            if (_currentTimeToRegenSuperPower >= _timeToRegenSuperPower)
            {
                _uiManager.UpdatePowerSlider(1);
                _soundManager.playSound(SoundManager.soundToPlay.superPower);
                _currentTimeToRegenSuperPower = 0.0f;
                _regenSuperPower = false;
                _superPowerActive = true;
            }
        }

        if(_superPowerActive && _inputManager.SuperPowerButtonPressed())
        {
            UseSuperPower();
            _superPowerActive = false;
            _regenSuperPower = true;
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
        _uiManager.UpdatePowerSlider(0);
        _shootingSuperBullet = true;
        GameObject aBullet = Instantiate(specialBulletPrefab, superBulletSpawn.transform.position, Quaternion.identity);
        Vector2 dir = new Vector2(-transform.position.x, -transform.position.z);
        aBullet.GetComponent<Bullet>().SetDirection(dir);
    }
}
