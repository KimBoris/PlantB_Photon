using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class ShopManager : MonoBehaviour
{
    //[���Ź�ư Ŭ��]
    bool isSlotOn;//������ ���ư��� ���̸� ��̱� ������Ű�� ���� �Լ�


    //[���� ������ �̹��� ����]
    SlotScr slotScr;

    //[���Ž� �ڽ� �ִϸ��̼�]
    shop_BoxScr shopBoxScr;

    public int itemNum; //�������� �̾��� ������ ����
    public string itemName;

    //[UI - ���â]    
    public GameObject ResultSet;    //���â UI��Ʈ
    public Text ResultText;         //��� ������ �ؽ�Ʈ
    public Image resultItemImg;      //��� ������ �̹���


    //[UI - ���� Ȯ��â]
    GameObject equipSlots;          //Instantiate - ��������

    public GameObject EquipSet;     //���� Ȯ��â UI��Ʈ
    public Text EquipText;          //���� ������ Ȯ�� �ؽ�Ʈ
    public Image EquipItemImg;      //���� ������ Ȯ�� �̹���
                                    //public Transform ResultPos;   //���â ��ġ
    public string SelectedEquipItemName; //�ش� ������ ��������Ʈ �̸��� �޾ƿ��� ���ڿ�

    //[UI - �������� â]
    string curEquipItem0;      //���� ������ ������
    string curEquipItem1;      //���� ������ ������
    string curEquipItem2;      //���� ������ ������
    Image curEquipItemImg0;
    Image curEquipItemImg1;
    Image curEquipItemImg2;
    public GameObject SelectedEquipItemObj; //���� ��ư Ŭ���� ������ ���ӿ�����Ʈ(Slotscr)���� �޾ƿ´�.

    //[���� ����]
    string releaseEquipItem0;   //���������� ������
    string releaseEquipItem1;   //���������� ������
    string releaseEquipItem2;   //���������� ������
    Image curReleaseEquipitem0;
    Image curReleaseEquipitem1;
    Image curReleaseEquipitem2;
    public GameObject ReleaseEquipItemName; //���� ��ư Ŭ���� �߰��� ���� ������Ʈ

    //[������]
    public int curCoin;
    public int curRuby;
    public Text curCoinText;
    public Text curRubyText;

    //�������� ������ ( ����� �����͸� �����Ű�� ���� ����Ʈ )From GameDataManager.instance.gameDate.haveItemName;
    List<string> startingItemNameList = new List<string>();

    //�������� ������ From GameDataManager.instance.gameData.equipItemName;
    //List<string> startingEquipItemNameList = new List<string>();
    //string[] startingEquipItemName = new string[3];
    //[UI - Slot]

    //[���� ����]
    public GameObject[] equipSlot = new GameObject[3];    //���� ���� ���â
    GameObject equipSlotParent; //�������� ����� �θ� ( Grid )
    Image equipSlotImg;      //�������� ����� �̹���

    //[�κ��丮 ����] ����X
    Image slotImg;           //������ �ڽ� �̹���
    GameObject slot;         //����
    GameObject slotParent;   //������ �θ� ( Grid )

    //[��ȭ ����]
    public GameObject RubySet;


    private void Awake()
    {
        GameDataManager.instance.LoadGameData();                                 //���� �����͸� Json���Ͽ��� ���� �ҷ��´�
        isSlotOn = false;
          curCoin = GameDataManager.instance.gameData.UserCoin;//������ ������ �ʱ�ȭ
        curRuby = GameDataManager.instance.gameData.UserRuby;//������ ��� �ʱ�ȭ
        curCoinText.text = string.Format("{0}", curCoin);
        curRubyText.text = string.Format("{0}", curRuby);
        shopBoxScr = GameObject.Find("BoxOfPandora").GetComponent<shop_BoxScr>();//�ڽ� �ִϸ��̼��� ����ϱ�����

        equipSlotParent = GameObject.Find("EquipSlotInven");

        slot = Resources.Load<GameObject>("Slot");                               //������ ���� ����
        slotParent = GameObject.Find("SlotParent(Grid)");                        //������ �θ�(�׸���)�� ã���ֱ�
    }
    void Start()
    {
        RefreshInventory();  //���ӵ����Ϳ� ����� �������� ��� ��������
        RefreshEquipInventory(); //���ӵ����Ϳ� ����� �������� ��� ��������
        RefreshValue(); //���� ��ȭ�� �ʱ�ȭ �ϱ� ���ؼ�
    }
    void Update()
    {

    }

    //[���� ��ư Ŭ��]
    public void BuyButtonDown() 
    {
        if (isSlotOn == false&& curRuby >= 100)
        {
            isSlotOn = true;
            itemNum = UnityEngine.Random.Range(1, 4);
            switch (itemNum)
            {
                case 1:
                    itemName = "Armor";
                    ResultText.text = "������ ȹ���Ͽ����ϴ�!";
                    break;
                case 2:
                    itemName = "Shoes";
                    ResultText.text = "�Ź��� ȹ���Ͽ����ϴ�!";
                    break;
                case 3:
                    itemName = "Cloak";
                    ResultText.text = "���Ǹ� ȹ���Ͽ����ϴ�!";
                    break;
            }
            StartCoroutine(ResultPopUpOn());
            //�ڽ� ���ڵ����̸� ���ؼ� �ڷ�ƾ���� ���
        }
        else if(curRuby <100)
        {
            NeedRubyPopup();
        }
    }

    //[�ڽ� �ִϸ��̼� + ���â ����]
    IEnumerator ResultPopUpOn()
    {
        curRuby -= 100; //��� 100����
        GameDataManager.instance.gameData.UserRuby = curRuby;
        GameDataManager.instance.SaveGameData();
        shopBoxScr.buyBox();//�ڽ� �ִϸ��̼�
        yield return new WaitForSeconds(2.5f);
        shopBoxScr.gameObject.SetActive(false);     //�ڽ� ������Ʈ ���� 
        ResultPopUp();//���â ����.
    }

    //[���â�� ���� ����]
    public void ResultPopUp()
    {
        ResultSet.SetActive(true);
        resultItemImg = GameObject.Find("ResultItemImg(Image)").GetComponent<Image>();//��� ������â�� �̹���
        resultItemImg.sprite = Resources.Load<Sprite>("Accessory/" + itemName);       //sprite������ ���ҽ������� ��� �̹����� ����
    }


    //[���â�� Ȯ�� ��ư�Է½� �κ��丮���� �������� �͵�]
    public void ResultConfirmBtn()
    {
        GameObject slots = Instantiate(slot, new Vector3(0, 0, 0), Quaternion.identity);//�κ��丮 ���ο� ���� ����
        slotImg = slots.transform.Find("ItemImage").GetComponent<Image>();              //������ ���� �̹��� ������Ʈ ��������
        slotImg.sprite = resultItemImg.sprite;                                          //�����̹����� ��������Ʈ ������������� ����

        slots.transform.SetParent(slotParent.transform);//slots�� �θ� ã���ֱ� (grid), �׸��忡 ����
        slots.transform.localScale = Vector3.one;       //slots�� ũ�� ����


        //getItemName.Add(itemName);//���� ����������� �̸��� getItemName(LIst) �� �߰� 
        //getItemName.Remove(itemName);//������ �Ǿ� ������ ������ �����ش�.
        GameDataManager.instance.gameData.haveItemName.Add(itemName);
        GameDataManager.instance.SaveGameData();
        ResultSet.SetActive(false);

        isSlotOn = false;
        SceneManager.LoadScene("Shop");
    }
    //[���â X��ư Ŭ��]
    public void ResultXButton()
    {
        ResultSet.SetActive(false);

        GameDataManager.instance.gameData.haveItemName.Add(itemName);
        GameDataManager.instance.SaveGameData();
        isSlotOn = false;
        SceneManager.LoadScene("Shop");
    }
    //[���â �Ǹ� ��ư Ŭ��]
    public void ResultSellButton()
    {
        ResultSet.SetActive(false);

        curCoin += 500; //500���� �����ְ�
        GameDataManager.instance.gameData.UserCoin = curCoin;
        GameDataManager.instance.SaveGameData();
        isSlotOn = false;
        SceneManager.LoadScene("Shop");
    }
    //[����۽� �κ��丮 ����]
    void RefreshInventory()
    {
        startingItemNameList.AddRange(GameDataManager.instance.gameData.haveItemName);
        //����� �����͸� ���絥���ͷ� �־��ֱ�
        if (startingItemNameList.Count != 0)
        {
            for (int i = 0; i < startingItemNameList.Count; i++)
            {
                GameObject slots = Instantiate(slot, new Vector3(0, 0, 0), Quaternion.identity); //slot�� ������ ������Ʈ slots
                slotImg = slots.transform.Find("ItemImage").GetComponent<Image>();               //���� �̹��� ������Ʈ ��������
                slotImg.sprite = Resources.Load<Sprite>("Accessory/" + startingItemNameList[i]); //���� �̹��� = ����� �������� string�� �´� �̹���
                slots.transform.SetParent(slotParent.transform);//slots�� �θ� ã���ֱ� (grid), �׸��忡 ����
                slots.transform.localScale = Vector3.one;       //slots�� ũ�� ����

                //GameDataManager.instance.gameData.equipItemName.Clear();
                //GameDataManager.instance.gameData.equipItemName.AddRange(startingItemNameList);
            }
            startingItemNameList.Clear();//�ߺ� ������ ���ؼ� ����Ʈ�� ����ش� 
            //�� �Լ����� ����� �����͸� �ҷ��ð��̱� ������
            GameDataManager.instance.gameData.haveItemName.AddRange(startingItemNameList);
        }
        GameDataManager.instance.SaveGameData();
    }

    void RefreshEquipInventory() //���� ������ ����۽� ����
    {
        curEquipItem0 = GameDataManager.instance.gameData.equiItemName0;//�κ��丮 0
        curEquipItem1 = GameDataManager.instance.gameData.equiItemName1;//�κ��丮 1
        curEquipItem2 = GameDataManager.instance.gameData.equiItemName2;//�κ��丮 2
        curEquipItemImg0 = equipSlot[0].GetComponent<Image>();
        curEquipItemImg1 = equipSlot[1].GetComponent<Image>();
        curEquipItemImg2 = equipSlot[2].GetComponent<Image>();
        if (!string.IsNullOrEmpty(curEquipItem0))
        {
            //equipSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + curEquipItem0);

            curEquipItemImg0.sprite = Resources.Load<Sprite>("Accessory/" + curEquipItem0);
            equipSlot[0].GetComponent<Image>().color = Vector4.one;
        }
        if (!string.IsNullOrEmpty(curEquipItem1))
        {
            //equipSlot[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + curEquipItem1);
            curEquipItemImg1.sprite = Resources.Load<Sprite>("Accessory/" + curEquipItem1);
            equipSlot[1].GetComponent<Image>().color = Vector4.one;
        }
        if (!string.IsNullOrEmpty(curEquipItem2))
        {
            //equipSlot[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + curEquipItem2);
            curEquipItemImg2.sprite = Resources.Load<Sprite>("Accessory/" + curEquipItem2);
            equipSlot[2].GetComponent<Image>().color = Vector4.one;
        }
        //GameDataManager.instance.SaveGameData();
    }



    //[UI - ���� Ȯ��â]
    public void SelectEquipmentItem()//�κ��丮���� ��� ����(Ŭ��)�� ��������â ����
    {
        EquipSet.SetActive(true);//�˾� > �����Ͻðڽ��ϱ�?
        //Slotscr���� ������ ������ �̹���, �ؽ�Ʈ �̹� ����
    }
    //[���� ��ư�� ������ ��]
    public void SelectEquipButton()
    {
        ChangeEquipImg();
        GameDataManager.instance.SaveGameData();
        EquipSet.SetActive(false);
        SceneManager.LoadScene("Shop");
    }
    //[������ ������ ó��]
    void ChangeEquipImg()
    {

        if (equipSlot[0].GetComponent<Image>().sprite == null)
        {
            equipSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            equipSlot[0].GetComponent<Image>().color = Vector4.one;
            GameDataManager.instance.gameData.equiItemName0 = SelectedEquipItemName;
            Destroy(SelectedEquipItemObj);
            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//�κ��丮���� �ش� �̸��� �������� ����

        }
        else if (equipSlot[1].GetComponent<Image>().sprite == null)
        {
            equipSlot[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            GameDataManager.instance.gameData.equiItemName1 = SelectedEquipItemName;
            Destroy(SelectedEquipItemObj);
            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//�κ��丮���� �ش� �̸��� �������� ����

        }
        else if (equipSlot[2].GetComponent<Image>().sprite == null)
        {
            equipSlot[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            equipSlot[2].GetComponent<Image>().color = Vector4.one;
            GameDataManager.instance.gameData.equiItemName2 = SelectedEquipItemName;
            Destroy(SelectedEquipItemObj);
            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//�κ��丮���� �ش� �̸��� �������� ����

        }
        else if (curEquipItemImg0.sprite != null && curEquipItemImg0.sprite != null && curEquipItemImg2.sprite != null)
        {
            equipSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            equipSlot[0].GetComponent<Image>().color = Vector4.one;

            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//�κ��丮���� �ش� �̸��� �������� ����
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName0);//�����Ǿ��ִ� �������� �߰�
            GameDataManager.instance.gameData.equiItemName0 = SelectedEquipItemName;     //�����ؼ� ������ �������� �̸��� ����
            return;
            //�±�ȯ
        }
    }
    public void DisMount0() //������ ��������
    {
        //�������θ� ���� â ����
        if (equipSlot[0].GetComponent<Image>().sprite != null)
        {
            equipSlot[0].GetComponent<Image>().color = Vector4.zero;//��Ţ����0���� ���� �����ش�.
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName0);//�κ��丮 ĭ�� ���â���ִ� ������ �߰�
            GameDataManager.instance.gameData.equiItemName0 = null; //����Ǿ��ִ� �����͸� �������ְ�
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Shop");
        }
    }
    public void DisMount1() //������ ��������
    {
        if (equipSlot[1].GetComponent<Image>().sprite != null)
        {
            equipSlot[1].GetComponent<Image>().color = Vector4.zero;//��Ţ����1���� ���� �����ش�.
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName1);//�κ��丮 ĭ�� ���â���ִ� ������ �߰�
            GameDataManager.instance.gameData.equiItemName1 = null; //����Ǿ��ִ� �����͸� �������ְ�
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Shop");
        }
    }
    public void DisMount2() //������ ��������
    {
        if (equipSlot[2].GetComponent<Image>().sprite != null)
        {
            equipSlot[2].GetComponent<Image>().color = Vector4.zero;//��Ţ����2���� ���� �����ش�.
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName2);//�κ��丮 ĭ�� ���â���ִ� ������ �߰�
            GameDataManager.instance.gameData.equiItemName2 = null; //����Ǿ��ִ� �����͸� �������ְ�
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Shop");
        }
    }

    public void SelectEquipSellItem()//���� ����â������ �ȱ� ��ư
    {
        GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//�����ߴ� ������ �����͸� ����
        curRuby += 500;
        GameDataManager.instance.gameData.UserRuby = curRuby;//����� �����Ϳ� ������ ��� ����
        GameDataManager.instance.gameData.UserCoin = curCoin;//����� �����Ϳ� ������ ���� ����
        GameDataManager.instance.SaveGameData();
        SceneManager.LoadScene("Shop");
    }
    void RefreshValue()
    {//���� ��ȭ�� �ʱ�ȭ
        curCoin = GameDataManager.instance.gameData.UserCoin;
        Debug.Log(curRuby);
        curRuby = GameDataManager.instance.gameData.UserRuby;

    }
    public void SelectEquipXButton()
    {
        EquipSet.SetActive(false);
    }

    public void NeedRubyPopup()//��� �����Ҷ� �ߴ� �˾�
    {
        RubySet.SetActive(true);//���â ����
    }

    public void NeedRubyPopupClose()
    {
        RubySet.SetActive(false);//���â �ݱ�
        isSlotOn = false;

    }

}



//public void SelectEquipButton()
//{
//    //���� ��ư�� ������ ��
//    slotScr = GameObject.Find("Slot(Clone)").GetComponent<SlotScr>();//����������Ʈ�� �ִ� �������� �����´�
//    GameDataManager.instance.gameData.equipItemName.Add(slotScr.ThisSlotImgName);//Slotscr���� ������ ������ �̸��� �����Ϳ� �߰�
//    //��ư ������ ���� �����Ϳ� �������� ����
//    GameDataManager.instance.SaveGameData();
//    SceneManager.LoadScene("Shop");
//}





//public void SelectEquipOkay()//���� ����â���� ����
//{
//    slotScr = GameObject.Find("Slot(Clone)").GetComponent<SlotScr>();//���Կ� �ִ� ������Ʈ�� �����´� ( ������ �̸��� ����ϱ����� )
//    //������ �������� ���ҽ����� �������� Resources.Load<Sprite>(slotScr.ThisSlotImgName);
//    Debug.Log(slotScr.ThisSlotImgName);
//    GameDataManager.instance.gameData.equipItemName.Add(slotScr.ThisSlotImgName);
//    GameDataManager.instance.SaveGameData();
//    EquipSet.SetActive(false);
//    SceneManager.LoadScene("Shop");
//}


//public void AddEquipItemInList()
//{//startingEquipItemName ����Ʈ�� �߰����� ������ �̸�



//    //GameDataManager.instance.gameData.equipItemName.Add(slotScr.ThisSlotImgName); //���� �����Ϳ� �������� �������� ����
//    //curEquipItem = GameObject.Find("EquipItemImage").GetComponent<Image>();
//    //curEquipItem.sprite = Resources.Load<Sprite>("Accessory/" + slotScr.ThisSlotImgName);//Slotscr�� ThisSlotImgName���� �ٲ��ش�.
//    //return;

//}


