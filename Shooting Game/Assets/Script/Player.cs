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


    private Vector2 inputVelocity;
    private Rigidbody2D rigid;
    private float shootCount;

    // Start is called before the first frame update
    void Start()
    {
        inputVelocity = Vector2.zero;
        rigid = GetComponent<Rigidbody2D>();
        shootCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shooting();
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
        if(collision.gameObject.tag == "Bullet")
        {
            hp -= 1;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

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

}
