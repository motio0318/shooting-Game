using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("弾オブジェクト")]
    protected GameObject[] bullet;

    [SerializeField, Header("弾を発射する時間")]
    protected float shootTime;

    [SerializeField, Header("体力")]
    private int hp;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    protected GameObject player;
    protected Rigidbody2D rigid;
    protected float shootCount;
    protected bool bAttack;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<Player>())
        {
            player = FindObjectOfType<Player>().gameObject;

        }

        shootCount = 0;
        bAttack = false;
        rigid = GetComponent<Rigidbody2D>();
        Initialize();
    }

    protected virtual void Initialize()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }


    //private void Shooting()
    //{
    //    if (player == null) return;

    //    shootCount += Time.deltaTime;
    //    if (shootCount < shootTime) return;

    //    GameObject bulletObj = Instantiate(bullet);
    //    bulletObj.transform.position = transform.position;
    //    Vector3 dir = player.transform.position - transform.position;
    //    bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
    //    shootCount = 0.0f;

    //}


    protected virtual void Attack()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hp -= 1;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

    protected virtual  void Move()
    {
        rigid.velocity = Vector2.down * moveSpeed;
    }

    private void OnWillRenderObject()
    {
        if(Camera.current.name == "Main Camera")
        {
            bAttack = true;
        }
    }

}
