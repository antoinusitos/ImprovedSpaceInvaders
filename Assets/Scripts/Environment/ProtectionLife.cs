using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionLife : MonoBehaviour
{
    public int life = 20;

    private void OnTriggerEnter(Collider collider)
    {
        PlayerBullet playerBullet = collider.transform.GetComponent<PlayerBullet>();
        if (playerBullet)
        {
            Destroy(collider.gameObject);
            life--;
            if(life <= 0)
            {
                Destroy(gameObject);
            }
            return;
        }
        Bullet bullet = collider.transform.GetComponent<Bullet>();
        if (bullet && collider.transform.tag == "EnemyBullet")
        {
            Destroy(collider.gameObject);
            life--;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


}
