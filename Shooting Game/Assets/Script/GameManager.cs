using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField, Header("’x‚­‚È‚éŽžŠÔ")]
    private float deadEffectTimeScale;
    [SerializeField, Header("ŽžŠÔ‚ðŒ³‚É–ß‚·ŽžŠÔ")]
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
