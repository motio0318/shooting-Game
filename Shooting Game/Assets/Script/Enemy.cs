using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("�e�I�u�W�F�N�g")]
    private GameObject bullet;

    [SerializeField, Header("�e�𔭎˂��鎞��")]
    private float shootTime;

    [SerializeField, Header("�̗�")]
    private int hp;


    private GameObject player;
    private float shootCount;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        shootCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }


    //parat5�Ōタ����Ƃ���

    private void Shooting()
    {
        if (player == null) return;

        shootCount += Time.deltaTime;
        if (shootCount < shootTime) return;

        GameObject bulletObj = Instantiate(bullet);
        bulletObj.transform.position = transform.position;
        Vector3 dir = player.transform.position - transform.position;
        bulletObj.transform.rotation = Quaternion.FromToRotation(transform.up, dir);
        shootCount = 0.0f;

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

}
