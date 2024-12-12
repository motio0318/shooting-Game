using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField, Header("移動範囲")]
    private float limitPosY;

    [SerializeField, Header("通常攻撃回数")]
    private int normalAttackCount;

    [SerializeField, Header("扇弾の弾数")]
    private int ougiBulletNum;

    [SerializeField, Header("扇の角度")]
    private float ougiAngle;

    [SerializeField, Header("扇弾の攻撃回数")]
    private int ougiAttackCount;

    [SerializeField, Header("ジグザググ弾攻撃の時間")]
    private float LRAttackTime;

    [SerializeField, Header("ジグザグ弾の間隔")]
    private float LRShootTime;

    [SerializeField, Header("ジグザグの幅")]
    private float LRRange;

    [SerializeField, Header("ジグザグの速度")]
    private float LRSpeed;


    enum AttackMode
    {
        Nomal,
        Ougi,
        LeftRight,
        Circle,
    }

    private int currentAttackCount;
    private AttackMode attackMode;
    private float rotateZ;
    private float LRAttackCount;

    protected override void Initialize()
    {
        currentAttackCount = 0;
        attackMode = AttackMode.Nomal;
        rotateZ = 0f;
        LRAttackCount = 0f;
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
            case AttackMode.Ougi :OugiShooting(); break;
            case AttackMode.LeftRight:LeftRightShooting(); break;
            case AttackMode.Circle: break;
        }
    }

    private void NomalShooting()
    {
        shootCount += Time.deltaTime;
        if (shootCount < shootTime) return;

        GameObject bulletObj = Instantiate(bullets[0]);
        bulletObj.transform.position = transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, Vector2.down);

        shootCount = 0f;
        currentAttackCount++;

        if(currentAttackCount >= normalAttackCount)
        {
            attackMode = AttackMode.Ougi;
            currentAttackCount = 0;
        }
    }

    private void OugiShooting()
    {
        shootCount += Time.deltaTime;
        if (shootCount < shootTime) return;

        for(int i = 0; i < ougiBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * ougiAngle;
            float theta = angleRange / (ougiBulletNum - 1) * i - Mathf.Deg2Rad * (90f + ougiAngle / 2f);
            GameObject bullet = Instantiate(bullets[1]);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);

        }

        shootCount = 0f;
        currentAttackCount++;

        if(currentAttackCount >= normalAttackCount)
        {
            attackMode = AttackMode.LeftRight;
            currentAttackCount = 0;
        }

    }

    private void LeftRightShooting()
    {
        LRAttackCount += Time.deltaTime;
        if(LRAttackCount >= LRAttackTime)
        {
            shootCount = 0f;
            LRAttackCount = 0f;
            attackMode = AttackMode.Circle;
        }


        shootCount += Time.deltaTime;
        if (shootCount < LRShootTime) return;

        rotateZ += LRSpeed;
        if(rotateZ > LRRange)
        {
            LRSpeed *= -1f;
            rotateZ = LRRange;
        }
        else if(rotateZ < -LRRange)
        {
            LRSpeed *= -1f;
            rotateZ  = -LRRange;
        }

        GameObject bullet = Instantiate(bullets[2]);
        bullet.transform.position = transform.position;
        bullet.transform.eulerAngles = new Vector3(0f, 0f, -180f + rotateZ);

        shootCount = 0f;
    }

}
