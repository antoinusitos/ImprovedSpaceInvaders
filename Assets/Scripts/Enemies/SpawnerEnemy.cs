using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        PlayerBullet playerBullet = collider.transform.GetComponent<PlayerBullet>();
        if (playerBullet)
        {
            Destroy(collider.gameObject);
        }
    }
}
