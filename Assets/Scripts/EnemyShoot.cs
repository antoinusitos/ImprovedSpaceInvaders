using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public bool _canShoot = false;

    private float _currentReload = 0.0f;
    public float fireRate = 4.0f;
    public float fireChance = 0.33f;

    public GameObject bulletPrefab;

    public void SetCanShoot(bool newState)
    {
        _canShoot = newState;
    }

    private void Update()
    {
        if(_canShoot)
        {
            _currentReload += Time.deltaTime;
            if(_currentReload >= fireRate)
            {
                _currentReload = 0.0f;
                if (Random.Range(0, 1.0f) <= fireChance)
                {
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 dir = new Vector2(transform.position.x, transform.position.z);
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}
