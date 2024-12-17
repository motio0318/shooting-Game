using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{

    protected override void Initialize()
    {
        moveVec = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 0f));
        Vector3 dir = (transform.position + (Vector3)(-moveVec)) - transform.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, dir);
    }
}
