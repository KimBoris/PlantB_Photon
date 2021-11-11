using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DrawAgainInShop : MonoBehaviour
{
    ItemPickUp itemPickUp;
    Item item;
    BuyButtonScr buyButtonScr;
    GameObject SaveResultItem;
    public GameObject ResultPopup;

    Image ResultItemImg;
    void Start()
    {
        //Load
        ResultPopup = GameObject.Find("ResultSet(Empty)");
        buyButtonScr = GameObject.Find("Menu(Button)").GetComponent<BuyButtonScr>();
    }

    void Update()
    {

    }

    public void AgreeShopScene()
    {//다시 뽑기시에 뽑은 아이템을 저장하고 샵 씬을 다시 불러오는 함수
        //저장할 데이터 베이스 및 슬롯에아이템 집어넣기
        Inventory inventory;

        inventory = FindObjectOfType<Inventory>();

        //inventory.AcquireItem(buyButtonScr.resultItem.GetComponent<ItemPickUp>().item, 1);

        ResultPopup.SetActive(false);//결과창 닫기
        //Save
        
        //여기에 데이터를 액세서리 뽑았을시 데이터를 저장해주는 코드 넣기
        
        //현재는 씬을 다시 불러오기 때문에 아이템이 저장되지 않는것 처럼 보인다.
        //SceneManager.LoadScene("Shop");
    }
    public void SellRusultItem()
    {
        //게임데이터에 코인 갯수 올려주고
        //슬롯에 갯수있는지 확인하고 처리.
    }


}
