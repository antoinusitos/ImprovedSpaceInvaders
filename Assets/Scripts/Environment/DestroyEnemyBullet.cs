using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyBullet : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        Bullet bullet = collider.transform.GetComponent<Bullet>();
        if (bullet)
        {
            Destroy(collider.gameObject);
        }
    }
}
