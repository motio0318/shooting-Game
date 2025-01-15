using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{


    [SerializeField, Header("スクロール速度")]
    private float speed;
    [SerializeField, Header("補正")]
    private float offset;

    private Vector2 cameraMinPos;
    private Vector2 cameraMaxPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraMinPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }
    private void Scroll()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        float cameraOutPosY = cameraMinPos.y * 3f;
        if(transform.position.y <= cameraOutPosY)
        {
            float resetPosY = cameraMaxPos.y * 3f;
            transform.position = new Vector3(0f, resetPosY - offset);
        }
    }
}
