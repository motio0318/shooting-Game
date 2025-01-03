using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField, Header("遅くなる時間")]
    private float deadEffectTimeScale;
    [SerializeField, Header("時間を元に戻す時間")]
    private float deadEffectTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeadEffect()
    {
        StartCoroutine(Slow());
    }

    IEnumerator Slow()
    {
        Time.timeScale = deadEffectTimeScale;

        yield return new WaitForSecondsRealtime(deadEffectTime);

        Time.timeScale = 1.0f;
    }
}
