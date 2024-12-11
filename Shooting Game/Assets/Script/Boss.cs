using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField, Header("ˆÚ“®”ÍˆÍ")]
    private float limitPosY;
    [SerializeField, Header("’ÊíUŒ‚‰ñ”")]
    private int normalAttackCount;

    enum AttackMode
    {
        Nomal,
        Ougi,
    }

    private int currentNomalAttackCount;
    private AttackMode attackMode;

    protected override void Initialize()
    {
        currentNomalAttackCount = 0;
        attackMode = AttackMode.Nomal;
    }

    protected override void Move()
    {
        if(transform.position.y <= limitPosY)
        {
            rigid.velocity = Vector2.zero;
            bAttack = true;
            return;
        }

        base.Move();
        bAttack = false;
    }

    protected override void Attack()
    {
        switch(attackMode)
        {
            case AttackMode.Nomal: NomalShooting();break;
            case AttackMode.Ougi : break;
        }
    }

    private void NomalShooting()
    {
        shootCount += Time.deltaTime;
        if (shootCount < shootTime) return;

        GameObject bulletObj = Instantiate(bullet[0]);
        bulletObj.transform.position = transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, Vector2.down);

        shootCount = 0f;
        currentNomalAttackCount++;

        if(currentNomalAttackCount >= normalAttackCount)
        {
            attackMode = AttackMode.Ougi;
            currentNomalAttackCount = 0;
        }
    }

}
