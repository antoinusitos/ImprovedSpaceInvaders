using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speedMovement = 2.0f;

    private bool _movingToNextLevel = false;

    private Vector3 newDir;

    public void SetDirection(Vector3 theDir, float theMovementSpeed)
    {
        newDir = theDir;
        speedMovement = theMovementSpeed;
        _movingToNextLevel = true;
    }

    public void SetMoving(bool newMoving)
    {
        _movingToNextLevel = newMoving;
    }

	void Update ()
    {
		if(_movingToNextLevel)
        {
            transform.position = Vector3.Lerp(transform.position, newDir, Time.deltaTime * speedMovement);
        }
	}
}
