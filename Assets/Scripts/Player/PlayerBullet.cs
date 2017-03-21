using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public ParticleSystem theParticleSystem;

    private GameObject _owner;

    public void SetOwner(GameObject theOwner)
    {
        _owner = theOwner;
    }

    public GameObject GetOwner()
    {
        return _owner;
    }

    private void Start()
    {
        if(theParticleSystem)
        {
            theParticleSystem.Play();
        }
    }
	
}
