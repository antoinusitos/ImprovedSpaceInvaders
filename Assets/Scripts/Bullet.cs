using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public int damage = 1;

    private Vector2 _direction;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDirection(Vector2 newDirection)
    {
        _direction = newDirection;
    }

    private void Update()
    {
        transform.position += new Vector3(_direction.x, 0, _direction.y) * speed * Time.deltaTime;
    }
	
}
