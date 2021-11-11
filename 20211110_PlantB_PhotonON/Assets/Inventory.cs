using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    //인벤토리 활성화 여부, true가 되면 카메라 움직임과 다른 입력을 막는다.

    [SerializeField]
    private GameObject go_InventoryBase; //Inventory_base이미지
    [SerializeField]
    private GameObject go_SlotsParent; //Slot들의 부모인 Grid Setting

    //private Slot[] slots;//슬롯들 배열
    void Start()
    {
      //  slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }
    public void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }


    //public void AcquireItem(Item _item, int _count = 1)
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        if (slots[i].item == null)
    //        {
    //            slots[i].AddItem(_item, _count);
    //            return;
    //        }
    //    }
    //    //장비가 아닐때
    //    //if (Item.ItemType.Equipment != _item.itemType)
    //    //{
    //        //for (int i = 0; i < slots.Length; i++)
    //        //{
    //        //    if (slots[i].item != null)
    //        //    {
    //        //        //null이라면 slots[i].item.itemName할 때 런타임 에러나기 때문에
    //        //        if (slots[i].item.itemName == _item.itemName)
    //        //        {
    //        //            slots[i].SetSlotCount(_count);
    //        //            return;
    //        //        }
    //        //    }
    //        //}
    //    //}
    //   //장비일때
    //}

}
