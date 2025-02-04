using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpIcon : MonoBehaviour
{
    [SerializeField, Header("HP�A�C�R��")]
    private GameObject hpIcon;

    private Player player;
    private int beforHP;
    private List<GameObject> hpIconList;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        beforHP = player.GetHP();
        hpIconList = new List<GameObject>();
        CreateHPIcon();
    }

    private void CreateHPIcon()
    {
        for(int i = 0; i < player.GetHP(); i++)
        {
            GameObject icon = Instantiate(hpIcon);
            icon.transform.SetParent(transform);
            hpIconList.Add(icon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowHPIcon();
    }

    private void ShowHPIcon()
    {
        if (beforHP == player.GetHP()) return;
        
        for(int i = 0; i < hpIconList.Count;i++)
        {
           hpIconList[i].SetActive(i < player.GetHP());
        }
        beforHP = player.GetHP();
        
    }
}
