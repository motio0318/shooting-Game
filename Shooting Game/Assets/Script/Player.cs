using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float speed;

    [SerializeField, Header("弾オブジェクト")]
    private GameObject bullet;

    [SerializeField, Header("弾を発射する時間")]
    private float shootTime;
    [SerializeField, Header("体力")]
    private int hp;
    [SerializeField, Header("点滅時間")]
    private float damageTime;
    [SerializeField, Header("点滅周期")]
    private float damageCycle;
    [SerializeField, Header("死亡エフェクト")]
    private GameObject deadEffect;



    private Vector2 inputVelocity;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private float shootCount;
    private float damageTimeCount;
    private bool bDamage;

    // Start is called before the first frame update
    void Start()
    {
        inputVelocity = Vector2.zero;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        shootCount = 0;
        damageTimeCount = 0;
        bDamage = false;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shooting();
        Damage();
    }
    private void Move()
    {
        rigid.velocity = inputVelocity * speed;
    }

    private void Shooting()
    {
        shootCount += Time.deltaTime;
        if (shootCount < shootTime) return;

        GameObject bulletObj = Instantiate(bullet);
        bulletObj.transform.position = transform.position + new Vector3(0f, transform.lossyScale.y / 2.0f, 0f);
        shootCount = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")
        {
            if(!bDamage)
            {
                hp -= 1;
                bDamage = true;
                if (hp <= 0)
                {
                    Destroy(gameObject);
                    Instantiate(deadEffect, transform.position, Quaternion.identity);
                    gameManager.DeadEffect();
                }

            }

        }
    }
     
    private void Damage()
    {
        if (!bDamage) return;

        damageTimeCount += Time.deltaTime;

        float value = Mathf.Repeat(damageTimeCount, damageCycle);
        spriteRenderer.enabled = value >= damageCycle * 0.5f;

        if(damageTimeCount >= damageTime)
        {
            damageTimeCount = 0;
            spriteRenderer.enabled = true;
            bDamage = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVelocity = context.ReadValue<Vector2>();
    }

    public int GetHP()
    {
        return hp;
    }


    public bool IsDamage()
    {
        return bDamage;
    }
}
