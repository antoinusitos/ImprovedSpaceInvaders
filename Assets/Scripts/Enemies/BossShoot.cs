using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject fireLocation;

    public float fireRate = 0.3f;
    private float _currentReload = 0.0f;

    private EnemyLife _enemyLife;

    enum state
    {
        waiting,
        rotating,
        firing,
    }

    private state _currentState;


    private void Start()
    {
        _enemyLife = GetComponent<EnemyLife>();
        _currentState = state.rotating;
    }

    private void Update()
    {
        _currentReload += Time.deltaTime;
        if(_currentReload >= fireRate)
        {
            _currentReload = 0.0f;
            GameObject bullet;
            if (_currentState == state.rotating)
            {
                bullet = Instantiate(bulletPrefab, fireLocation.transform.position, Quaternion.identity);
                Vector3 localForward = transform.InverseTransformDirection(transform.forward);
                Vector2 dir = new Vector2(fireLocation.transform.right.x, fireLocation.transform.right.z);
                bullet.GetComponent<Bullet>().SetDirection(dir);
            }
            else if (_currentState == state.firing)
            {
                bullet = Instantiate(bulletPrefab, fireLocation.transform.position, Quaternion.identity);
                Vector2 dir = new Vector2(0.0f, 1.0f);
                bullet.GetComponent<Bullet>().SetDirection(dir);
                bullet = Instantiate(bulletPrefab, fireLocation.transform.position, Quaternion.identity);
                dir = new Vector2(0.0f, -1.0f);
                bullet.GetComponent<Bullet>().SetDirection(dir);
                bullet = Instantiate(bulletPrefab, fireLocation.transform.position, Quaternion.identity);
                dir = new Vector2(1.0f, 0.0f);
                bullet.GetComponent<Bullet>().SetDirection(dir);
                bullet = Instantiate(bulletPrefab, fireLocation.transform.position, Quaternion.identity);
                dir = new Vector2(-1.0f, 0.0f);
                bullet.GetComponent<Bullet>().SetDirection(dir);
            }
            
        }

        if(_enemyLife.life <= 300)
        {
            _currentState = state.firing;
            GetComponent<BossMovement>().SetCanMove(false);
        }
    }
}
