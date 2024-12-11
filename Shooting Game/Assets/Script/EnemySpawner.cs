using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("敵オブジェクト")]
    private GameObject[] enemy;
    [SerializeField,Header("敵を生成する時間")]
    private float[] spawnTimes;


    private float spawnCount;
    private int   spawnNum;
    // Start is called before the first frame update
    void Start()
    {
        spawnCount = 0.0f;
        spawnNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (spawnNum > enemy.Length - 1) return;

        spawnCount += Time.deltaTime;
        if(spawnCount >= spawnTimes[spawnNum])
        {
            Instantiate(enemy[spawnNum]);
            spawnNum++;
            spawnCount = 0.0f;
        }
    }
}
