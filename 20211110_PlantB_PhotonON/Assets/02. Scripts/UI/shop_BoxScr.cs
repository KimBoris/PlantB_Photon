using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_BoxScr : MonoBehaviour
{
    ShopManager shopManager;

    Animator boxAnim;
    BuyButtonScr buyButtonScr;//�ڽ� �ִϸ��̼� ���� ��ũ��Ʈ
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
        //�ڽ� �ִϸ��̼�
        boxAnim.SetTrigger("isBuy");
    }
}
