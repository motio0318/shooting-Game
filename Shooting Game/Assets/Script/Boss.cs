using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField, Header("�ړ��͈�")]
    private float limitPosY;

    [SerializeField, Header("�ʏ�U����")]
    private int normalAttackCount;

    [SerializeField, Header("��e�̒e��")]
    private int ougiBulletNum;

    [SerializeField, Header("��̊p�x")]
    private float ougiAngle;

    [SerializeField, Header("��e�̍U����")]
    private int ougiAttackCount;

    [SerializeField, Header("�W�O�U�O�O�e�U���̎���")]
    private float LRAttackTime;

    [SerializeField, Header("�W�O�U�O�e�̊Ԋu")]
    private float LRShootTime;

    [SerializeField, Header("�W�O�U�O�̕�")]
    private float LRRange;

    [SerializeField, Header("�W�O�U�O�̑��x")]
    private float LRSpeed;
    [SerializeField, Header("�~�`�̒e��")]
    private int circleBulletNum;
    [SerializeField, Header("�~�`�U���̎���")]
    private float circleShootTime;
    [SerializeField, Header("�~�`�ɒe�𔭎˂���Ԋu")]
    private float circleBulletTime;


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
    private float circleShootCount;

    protected override void Initialize()
    {
        currentAttackCount = 0;
        attackMode = AttackMode.Nomal;
        rotateZ = 0f;
        LRAttackCount = 0f;
        circleShootCount = 0f;
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
            case AttackMode.Circle:CircleShooting(); break;
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


    private void CircleShooting()
    {
        circleShootCount += Time.deltaTime;
        if(circleShootCount >= circleShootTime)
        {
            shootCount = 0f;
            circleShootCount= 0f;
            attackMode = AttackMode.Nomal;
        }

        shootCount += Time.deltaTime;
        if (shootCount < circleBulletTime) return;

        for(int i = 0; i < circleBulletNum; i++)
        {
            float angleRange = Mathf.Deg2Rad * 360f;
            float theta = angleRange / circleBulletNum  * i - Mathf.Deg2Rad * (90f + 360f / 2f);

            GameObject bullet = Instantiate(bullets[3]);
            bullet.transform.position = transform.position;
            Vector3 dir = transform.position + new Vector3(Mathf.Cos(theta), Mathf.Sin(theta)) - transform.position;
            bullet.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        }
        shootCount = 0f;
    }
}
