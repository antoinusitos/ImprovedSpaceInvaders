using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float fireRate = 0.2f;
    private float _currentReload = 0.0f;
    private bool _canShoot = true;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    //accessor
    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.GetInstance();
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
    }
}
