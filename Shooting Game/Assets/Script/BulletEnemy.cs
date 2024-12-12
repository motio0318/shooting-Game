using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Enemy
{
    protected override void Attack()
    {

        Shooting();
    }

    private void Shooting()
    {
        if (player == null) return;

        shootCount += Time.deltaTime;
        if (shootCount < shootTime) return;

        GameObject bulletObj = Instantiate(bullets[0]);
        bulletObj.transform.position = transform.position;
        Vector3 dir = player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        shootCount = 0.0f;

    }



}
