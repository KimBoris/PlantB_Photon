using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SlotScr : MonoBehaviour
{
    //1. �� ������Ʈ�� �̹����� ����ϱ� ���ؼ� string������ �̸��� ���´�.
    public string ThisSlotImgName;// ������ �̹��� �̸�
    GameObject ThisSlotObj; //���� ������Ʈ
    ShopManager shopManager;


    private void OnEnable()
    {
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
    }
    void Start()
    {
        
    }

    //[�κ��丮�� �ִ� �������� Ŭ��������]
    public void InventoryItemClick()
    {
        //�̹����� �̸��� ��ȯ
        //ThisSlotImgName = this.gameObject.transform.Find("ItemImage").GetComponent<Image>().sprite.name;

        shopManager.SelectedEquipItemObj = EventSystem.current.currentSelectedGameObject;
        ThisSlotImgName = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite.name;
        //Ŭ���� ��ư�� ��������Ʈ �̸� 
        shopManager.SelectedEquipItemName = ThisSlotImgName; //shopManager���� ����ϱ� ���� ����ش�
        //EquipSet(True) 
        shopManager.SelectEquipmentItem();//��������â ����
        shopManager.EquipItemImg.sprite = Resources.Load<Sprite>("Accessory/"+ ThisSlotImgName);//��������â�� �̹���
        EquipItemText();//���� ����â�� �ؽ�Ʈ
    }

    void EquipItemText()
    {
        switch (ThisSlotImgName)
        {
            case "Armor":
                shopManager.EquipText.text = "<����>\nü�� 30% ����";
                break;
            case "Shoes":
                shopManager.EquipText.text = "<�Ź�>\n�̵��ӵ� 30% ����";
                break;
            case "Cloak":
                shopManager.EquipText.text = "<����>\n��������� 20%����";
                break;
        }
    }
}
