using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_BoxScr : MonoBehaviour
{
    ShopManager shopManager;

    Animator boxAnim;
    BuyButtonScr buyButtonScr;//박스 애니메이션 이후 스크립트
    void Start()
    {
        boxAnim = GetComponent<Animator>();
        buyButtonScr = FindObjectOfType<BuyButtonScr>();
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }

    void Update()
    {

    }
    public void buyBox()
    {
        //박스 애니메이션
        boxAnim.SetTrigger("isBuy");
    }
}
