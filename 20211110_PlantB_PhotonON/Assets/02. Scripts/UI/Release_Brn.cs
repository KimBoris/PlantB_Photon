using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Release_Brn : MonoBehaviour
{
    //������Ʈ �θ��� �̹��� �̸�
    string ReleaseItemName;
    ShopManager shopManager;


    private void OnEnable()
    {
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }
    void Start()
    {

    }


    public void ReleaseItemClick()//���� ��ư�� ������
    {
        shopManager.
        ReleaseEquipItemName = EventSystem.current.currentSelectedGameObject.GetComponentInParent<GameObject>();
       // ReleaseItemName = shopManager.ReleaseEquipItemName;


    }
}
