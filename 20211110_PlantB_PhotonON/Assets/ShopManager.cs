using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class ShopManager : MonoBehaviour
{
    //[구매버튼 클릭]
    bool isSlotOn;//슬롯이 돌아가는 중이면 재뽑기 금지시키기 위한 함수


    //[슬롯 장착시 이미지 구현]
    SlotScr slotScr;

    //[구매시 박스 애니메이션]
    shop_BoxScr shopBoxScr;

    public int itemNum; //랜덤으로 뽑아줄 아이템 숫자
    public string itemName;

    //[UI - 결과창]    
    public GameObject ResultSet;    //결과창 UI세트
    public Text ResultText;         //결과 아이템 텍스트
    public Image resultItemImg;      //결과 아이템 이미지


    //[UI - 장착 확인창]
    GameObject equipSlots;          //Instantiate - 장착슬롯

    public GameObject EquipSet;     //장착 확인창 UI세트
    public Text EquipText;          //장착 아이템 확인 텍스트
    public Image EquipItemImg;      //장착 아이템 확인 이미지
                                    //public Transform ResultPos;   //결과창 위치
    public string SelectedEquipItemName; //해당 슬롯의 스프라이트 이름을 받아오는 문자열

    //[UI - 장착중인 창]
    string curEquipItem0;      //현재 착용한 아이템
    string curEquipItem1;      //현재 착용한 아이템
    string curEquipItem2;      //현재 착용한 아이템
    Image curEquipItemImg0;
    Image curEquipItemImg1;
    Image curEquipItemImg2;
    public GameObject SelectedEquipItemObj; //장착 버튼 클릭시 삭제할 게임오브젝트(Slotscr)에서 받아온다.

    //[장착 해제]
    string releaseEquipItem0;   //장착해제할 아이템
    string releaseEquipItem1;   //장착해제할 아이템
    string releaseEquipItem2;   //장착해제할 아이템
    Image curReleaseEquipitem0;
    Image curReleaseEquipitem1;
    Image curReleaseEquipitem2;
    public GameObject ReleaseEquipItemName; //장착 버튼 클릭시 추가할 게임 오브젝트

    //[데이터]
    public int curCoin;
    public int curRuby;
    public Text curCoinText;
    public Text curRubyText;

    //보유중인 아이템 ( 저장된 데이터를 적용시키기 위한 리스트 )From GameDataManager.instance.gameDate.haveItemName;
    List<string> startingItemNameList = new List<string>();

    //장착중인 아이템 From GameDataManager.instance.gameData.equipItemName;
    //List<string> startingEquipItemNameList = new List<string>();
    //string[] startingEquipItemName = new string[3];
    //[UI - Slot]

    //[장착 슬롯]
    public GameObject[] equipSlot = new GameObject[3];    //장착 중인 장비창
    GameObject equipSlotParent; //장착중인 장비의 부모 ( Grid )
    Image equipSlotImg;      //장착중인 장비의 이미지

    //[인벤토리 슬롯] 장착X
    Image slotImg;           //슬롯의 자식 이미지
    GameObject slot;         //슬롯
    GameObject slotParent;   //슬롯의 부모 ( Grid )

    //[재화 부족]
    public GameObject RubySet;


    private void Awake()
    {
        GameDataManager.instance.LoadGameData();                                 //게임 데이터를 Json파일에서 부터 불러온다
        isSlotOn = false;
          curCoin = GameDataManager.instance.gameData.UserCoin;//현재의 코인을 초기화
        curRuby = GameDataManager.instance.gameData.UserRuby;//현재의 루비를 초기화
        curCoinText.text = string.Format("{0}", curCoin);
        curRubyText.text = string.Format("{0}", curRuby);
        shopBoxScr = GameObject.Find("BoxOfPandora").GetComponent<shop_BoxScr>();//박스 애니메이션을 사용하기위해

        equipSlotParent = GameObject.Find("EquipSlotInven");

        slot = Resources.Load<GameObject>("Slot");                               //슬롯의 정의 설정
        slotParent = GameObject.Find("SlotParent(Grid)");                        //슬롯의 부모(그리드)를 찾아주기
    }
    void Start()
    {
        RefreshInventory();  //게임데이터에 저장된 보유중인 장비 가져오기
        RefreshEquipInventory(); //게임데이터에 저장된 장착중인 장비 가져오기
        RefreshValue(); //돈과 재화를 초기화 하기 위해서
    }
    void Update()
    {

    }

    //[구매 버튼 클릭]
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
                    ResultText.text = "갑옷을 획득하였습니다!";
                    break;
                case 2:
                    itemName = "Shoes";
                    ResultText.text = "신발을 획득하였습니다!";
                    break;
                case 3:
                    itemName = "Cloak";
                    ResultText.text = "망또를 획득하였습니다!";
                    break;
            }
            StartCoroutine(ResultPopUpOn());
            //박스 상자딜레이를 위해서 코루틴으로 사용
        }
        else if(curRuby <100)
        {
            NeedRubyPopup();
        }
    }

    //[박스 애니메이션 + 결과창 띄우기]
    IEnumerator ResultPopUpOn()
    {
        curRuby -= 100; //루비 100차감
        GameDataManager.instance.gameData.UserRuby = curRuby;
        GameDataManager.instance.SaveGameData();
        shopBoxScr.buyBox();//박스 애니메이션
        yield return new WaitForSeconds(2.5f);
        shopBoxScr.gameObject.SetActive(false);     //박스 오브젝트 끄기 
        ResultPopUp();//결과창 띄운다.
    }

    //[결과창을 띄우는 역할]
    public void ResultPopUp()
    {
        ResultSet.SetActive(true);
        resultItemImg = GameObject.Find("ResultItemImg(Image)").GetComponent<Image>();//결과 아이템창의 이미지
        resultItemImg.sprite = Resources.Load<Sprite>("Accessory/" + itemName);       //sprite형태의 리소스파일을 결과 이미지로 적용
    }


    //[결과창의 확인 버튼입력시 인벤토리에서 벌어지는 것들]
    public void ResultConfirmBtn()
    {
        GameObject slots = Instantiate(slot, new Vector3(0, 0, 0), Quaternion.identity);//인벤토리 내부에 슬롯 생성
        slotImg = slots.transform.Find("ItemImage").GetComponent<Image>();              //생성된 슬롯 이미지 컴포넌트 가져오기
        slotImg.sprite = resultItemImg.sprite;                                          //슬롯이미지의 스프라이트 결과아이템으로 적용

        slots.transform.SetParent(slotParent.transform);//slots의 부모 찾아주기 (grid), 그리드에 안착
        slots.transform.localScale = Vector3.one;       //slots의 크기 조정


        //getItemName.Add(itemName);//얻은 결과아이템의 이름을 getItemName(LIst) 에 추가 
        //getItemName.Remove(itemName);//누적이 되어 버리기 때문에 지워준다.
        GameDataManager.instance.gameData.haveItemName.Add(itemName);
        GameDataManager.instance.SaveGameData();
        ResultSet.SetActive(false);

        isSlotOn = false;
        SceneManager.LoadScene("Shop");
    }
    //[결과창 X버튼 클릭]
    public void ResultXButton()
    {
        ResultSet.SetActive(false);

        GameDataManager.instance.gameData.haveItemName.Add(itemName);
        GameDataManager.instance.SaveGameData();
        isSlotOn = false;
        SceneManager.LoadScene("Shop");
    }
    //[결과창 판매 버튼 클릭]
    public void ResultSellButton()
    {
        ResultSet.SetActive(false);

        curCoin += 500; //500원을 더해주고
        GameDataManager.instance.gameData.UserCoin = curCoin;
        GameDataManager.instance.SaveGameData();
        isSlotOn = false;
        SceneManager.LoadScene("Shop");
    }
    //[재시작시 인벤토리 리셋]
    void RefreshInventory()
    {
        startingItemNameList.AddRange(GameDataManager.instance.gameData.haveItemName);
        //저장된 데이터를 현재데이터로 넣어주기
        if (startingItemNameList.Count != 0)
        {
            for (int i = 0; i < startingItemNameList.Count; i++)
            {
                GameObject slots = Instantiate(slot, new Vector3(0, 0, 0), Quaternion.identity); //slot을 적용한 오브젝트 slots
                slotImg = slots.transform.Find("ItemImage").GetComponent<Image>();               //슬롯 이미지 컴포넌트 가져오기
                slotImg.sprite = Resources.Load<Sprite>("Accessory/" + startingItemNameList[i]); //슬롯 이미지 = 저장된 데이터의 string에 맞는 이미지
                slots.transform.SetParent(slotParent.transform);//slots의 부모 찾아주기 (grid), 그리드에 안착
                slots.transform.localScale = Vector3.one;       //slots의 크기 조정

                //GameDataManager.instance.gameData.equipItemName.Clear();
                //GameDataManager.instance.gameData.equipItemName.AddRange(startingItemNameList);
            }
            startingItemNameList.Clear();//중복 방지를 위해서 리스트를 비워준다 
            //이 함수에서 저장된 데이터를 불러올것이기 때문에
            GameDataManager.instance.gameData.haveItemName.AddRange(startingItemNameList);
        }
        GameDataManager.instance.SaveGameData();
    }

    void RefreshEquipInventory() //장착 아이템 재시작시 설정
    {
        curEquipItem0 = GameDataManager.instance.gameData.equiItemName0;//인벤토리 0
        curEquipItem1 = GameDataManager.instance.gameData.equiItemName1;//인벤토리 1
        curEquipItem2 = GameDataManager.instance.gameData.equiItemName2;//인벤토리 2
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



    //[UI - 장착 확인창]
    public void SelectEquipmentItem()//인벤토리에서 장비 선택(클릭)시 장착여부창 열기
    {
        EquipSet.SetActive(true);//팝업 > 장착하시겠습니까?
        //Slotscr에서 장착할 아이템 이미지, 텍스트 이미 구현
    }
    //[장착 버튼을 눌렀을 때]
    public void SelectEquipButton()
    {
        ChangeEquipImg();
        GameDataManager.instance.SaveGameData();
        EquipSet.SetActive(false);
        SceneManager.LoadScene("Shop");
    }
    //[아이템 장착시 처리]
    void ChangeEquipImg()
    {

        if (equipSlot[0].GetComponent<Image>().sprite == null)
        {
            equipSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            equipSlot[0].GetComponent<Image>().color = Vector4.one;
            GameDataManager.instance.gameData.equiItemName0 = SelectedEquipItemName;
            Destroy(SelectedEquipItemObj);
            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//인벤토리에서 해당 이름의 아이템을 삭제

        }
        else if (equipSlot[1].GetComponent<Image>().sprite == null)
        {
            equipSlot[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            GameDataManager.instance.gameData.equiItemName1 = SelectedEquipItemName;
            Destroy(SelectedEquipItemObj);
            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//인벤토리에서 해당 이름의 아이템을 삭제

        }
        else if (equipSlot[2].GetComponent<Image>().sprite == null)
        {
            equipSlot[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            equipSlot[2].GetComponent<Image>().color = Vector4.one;
            GameDataManager.instance.gameData.equiItemName2 = SelectedEquipItemName;
            Destroy(SelectedEquipItemObj);
            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//인벤토리에서 해당 이름의 아이템을 삭제

        }
        else if (curEquipItemImg0.sprite != null && curEquipItemImg0.sprite != null && curEquipItemImg2.sprite != null)
        {
            equipSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Accessory/" + SelectedEquipItemName);
            equipSlot[0].GetComponent<Image>().color = Vector4.one;

            GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//인벤토리에서 해당 이름의 아이템을 삭제
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName0);//장착되어있던 아이템을 추가
            GameDataManager.instance.gameData.equiItemName0 = SelectedEquipItemName;     //선택해서 장착한 아이템의 이름을 저장
            return;
            //맞교환
        }
    }
    public void DisMount0() //아이템 장착해제
    {
        //해제여부를 묻는 창 생성
        if (equipSlot[0].GetComponent<Image>().sprite != null)
        {
            equipSlot[0].GetComponent<Image>().color = Vector4.zero;//이큅슬롯0번의 값을 지워준다.
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName0);//인벤토리 칸에 장비창에있던 아이템 추가
            GameDataManager.instance.gameData.equiItemName0 = null; //저장되어있던 데이터를 삭제해주고
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Shop");
        }
    }
    public void DisMount1() //아이템 장착해제
    {
        if (equipSlot[1].GetComponent<Image>().sprite != null)
        {
            equipSlot[1].GetComponent<Image>().color = Vector4.zero;//이큅슬롯1번의 값을 지워준다.
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName1);//인벤토리 칸에 장비창에있던 아이템 추가
            GameDataManager.instance.gameData.equiItemName1 = null; //저장되어있던 데이터를 삭제해주고
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Shop");
        }
    }
    public void DisMount2() //아이템 장착해제
    {
        if (equipSlot[2].GetComponent<Image>().sprite != null)
        {
            equipSlot[2].GetComponent<Image>().color = Vector4.zero;//이큅슬롯2번의 값을 지워준다.
            GameDataManager.instance.gameData.haveItemName.Add(GameDataManager.instance.gameData.equiItemName2);//인벤토리 칸에 장비창에있던 아이템 추가
            GameDataManager.instance.gameData.equiItemName2 = null; //저장되어있던 데이터를 삭제해주고
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Shop");
        }
    }

    public void SelectEquipSellItem()//장착 여부창에서의 팔기 버튼
    {
        GameDataManager.instance.gameData.haveItemName.Remove(SelectedEquipItemName);//선택했던 아이템 데이터를 삭제
        curRuby += 500;
        GameDataManager.instance.gameData.UserRuby = curRuby;//저장된 데이터에 현재의 루비를 대입
        GameDataManager.instance.gameData.UserCoin = curCoin;//저장된 데이터에 현재의 돈을 대입
        GameDataManager.instance.SaveGameData();
        SceneManager.LoadScene("Shop");
    }
    void RefreshValue()
    {//돈과 재화를 초기화
        curCoin = GameDataManager.instance.gameData.UserCoin;
        Debug.Log(curRuby);
        curRuby = GameDataManager.instance.gameData.UserRuby;

    }
    public void SelectEquipXButton()
    {
        EquipSet.SetActive(false);
    }

    public void NeedRubyPopup()//루비가 부족할때 뜨는 팝업
    {
        RubySet.SetActive(true);//루비창 열기
    }

    public void NeedRubyPopupClose()
    {
        RubySet.SetActive(false);//루비창 닫기
        isSlotOn = false;

    }

}



//public void SelectEquipButton()
//{
//    //장착 버튼을 눌렀을 때
//    slotScr = GameObject.Find("Slot(Clone)").GetComponent<SlotScr>();//슬롯컴포넌트에 있는 아이템을 가져온다
//    GameDataManager.instance.gameData.equipItemName.Add(slotScr.ThisSlotImgName);//Slotscr에서 선정된 아이템 이름을 데이터에 추가
//    //버튼 누를시 게임 데이터에 아이템을 저장
//    GameDataManager.instance.SaveGameData();
//    SceneManager.LoadScene("Shop");
//}





//public void SelectEquipOkay()//장착 여부창에서 장착
//{
//    slotScr = GameObject.Find("Slot(Clone)").GetComponent<SlotScr>();//슬롯에 있는 컴포넌트를 가져온다 ( 아이템 이름을 사용하기위해 )
//    //선택한 아이템의 리소스파일 가져오기 Resources.Load<Sprite>(slotScr.ThisSlotImgName);
//    Debug.Log(slotScr.ThisSlotImgName);
//    GameDataManager.instance.gameData.equipItemName.Add(slotScr.ThisSlotImgName);
//    GameDataManager.instance.SaveGameData();
//    EquipSet.SetActive(false);
//    SceneManager.LoadScene("Shop");
//}


//public void AddEquipItemInList()
//{//startingEquipItemName 리스트에 추가해줄 아이템 이름



//    //GameDataManager.instance.gameData.equipItemName.Add(slotScr.ThisSlotImgName); //게임 데이터에 장착중인 아이템을 저장
//    //curEquipItem = GameObject.Find("EquipItemImage").GetComponent<Image>();
//    //curEquipItem.sprite = Resources.Load<Sprite>("Accessory/" + slotScr.ThisSlotImgName);//Slotscr의 ThisSlotImgName으로 바꿔준다.
//    //return;

//}


