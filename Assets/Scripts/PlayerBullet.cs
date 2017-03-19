using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public ParticleSystem particleSystem;

    private void Start()
    {
        if(particleSystem)
        {
            particleSystem.Play();
        }
    }
	
}
