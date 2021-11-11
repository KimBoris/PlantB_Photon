using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Release_Brn : MonoBehaviour
{
    //오브젝트 부모의 이미지 이름
    string ReleaseItemName;
    ShopManager shopManager;


    private void OnEnable()
    {
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }
    void Start()
    {

    }


    public void ReleaseItemClick()//해제 버튼을 누르면
    {
        shopManager.
        ReleaseEquipItemName = EventSystem.current.currentSelectedGameObject.GetComponentInParent<GameObject>();
       // ReleaseItemName = shopManager.ReleaseEquipItemName;


    }
}
