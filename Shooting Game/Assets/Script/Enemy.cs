using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("�e�I�u�W�F�N�g")]
    protected GameObject[] bullets;

    [SerializeField, Header("�e�𔭎˂��鎞��")]
    protected float shootTime;

    [SerializeField, Header("�̗�")]
    private int hp;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;

    [SerializeField, Header("�_���[�W�G�t�F�N�g�̎���")]
    private float damageEffectTime;
    [SerializeField, Header("�_���[�W���̉摜")]
    private Sprite damageSprite;

    protected GameObject player;
    protected Rigidbody2D rigid;
    protected Vector2 moveVec;
    protected float shootCount;
    protected bool bAttack;

    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        moveVec = Vector2.down;
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
            hp -= collision.GetComponent<Bullet>().GetPower();
            StartCoroutine(Damage());
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

    protected virtual  void Move()
    {
        rigid.velocity = moveVec * moveSpeed;
    }

    private void OnWillRenderObject()
    {
        if(Camera.current.name == "Main Camera")
        {
            bAttack = true;
        }
    }

    private IEnumerator Damage()
    {
        spriteRenderer.sprite = damageSprite;

        yield return new WaitForSeconds(damageEffectTime);

        spriteRenderer.sprite = defaultSprite;
    }

}
