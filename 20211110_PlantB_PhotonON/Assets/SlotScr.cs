using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SlotScr : MonoBehaviour
{
    //1. 이 오브젝트의 이미지를 사용하기 위해서 string형으로 이름은 따온다.
    public string ThisSlotImgName;// 슬롯의 이미지 이름
    GameObject ThisSlotObj; //슬롯 오브젝트
    ShopManager shopManager;


    private void OnEnable()
    {
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }
    void Start()
    {
        
    }

    //[인벤토리에 있는 아이템을 클릭했을때]
    public void InventoryItemClick()
    {
        //이미지의 이름을 반환
        //ThisSlotImgName = this.gameObject.transform.Find("ItemImage").GetComponent<Image>().sprite.name;

        shopManager.SelectedEquipItemObj = EventSystem.current.currentSelectedGameObject;
        ThisSlotImgName = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;
        //클릭한 버튼의 스프라이트 이름 
        shopManager.SelectedEquipItemName = ThisSlotImgName; //shopManager에서 사용하기 위해 담아준다
        //EquipSet(True) 
        shopManager.SelectEquipmentItem();//장착여부창 띄우기
        shopManager.EquipItemImg.sprite = Resources.Load<Sprite>("Accessory/"+ ThisSlotImgName);//장착여부창의 이미지
        EquipItemText();//장착 여부창의 텍스트
    }

    void EquipItemText()
    {
        switch (ThisSlotImgName)
        {
            case "Armor":
                shopManager.EquipText.text = "<갑옷>\n체력 30% 증가";
                break;
            case "Shoes":
                shopManager.EquipText.text = "<신발>\n이동속도 30% 증가";
                break;
            case "Cloak":
                shopManager.EquipText.text = "<망토>\n마나재생력 20%증가";
                break;
        }
    }
}
