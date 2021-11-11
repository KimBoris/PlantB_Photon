using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{


    P_Hatch_Status player;
    //P_Casper_Status playerCasper;
    public Image hpBar;  //Ã¼·Â¹Ù
    public Image mpBar;  //¸¶³ª¹Ù

    public Text gunState;//ÃÑ ÇöÀç Åº¾Ë
    public Image gunImage; //ÃÑ ÇöÀç Åº¾Ë UI
    void Start()
    {
        //player = GameObject.Find("Hatch(Clone)").GetComponent<P_Hatch_Status>();
        //gunImage = GameObject.Find("BulletsFill").GetComponent<Image>();
    }
    void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.Find("Hatch(Clone)").GetComponent<P_Hatch_Status>();
            //GunState();
            HpMpbar();
        }
    }
    //void GunState() //ÇöÀç ÃÑ¾Ë °¹¼ö
    //{
    //    rifle = GameObject.Find("w_rifle").GetComponent<Rifle>(); ;
    //    gunImage.color = Color.yellow;
    //    gunImage.fillAmount = (rifle.remainBullet / rifle.capacity);
    //    gunState.text = string.Format("{0}", rifle.remainBullet);
    //    if (rifle.remainBullet < 11)
    //    {
    //        gunImage.color = Color.red;
    //    }
    //}
    void HpMpbar()  //HpMp»óÅÂ UI
    {
        hpBar.fillAmount = (player.currHp / player.maxHp);
        mpBar.fillAmount = (player.currMp / player.maxMp);
    }

}
