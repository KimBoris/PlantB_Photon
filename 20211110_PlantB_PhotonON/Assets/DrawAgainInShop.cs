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
    {//�ٽ� �̱�ÿ� ���� �������� �����ϰ� �� ���� �ٽ� �ҷ����� �Լ�
        //������ ������ ���̽� �� ���Կ������� ����ֱ�
        Inventory inventory;

        inventory = FindObjectOfType<Inventory>();

        //inventory.AcquireItem(buyButtonScr.resultItem.GetComponent<ItemPickUp>().item, 1);

        ResultPopup.SetActive(false);//���â �ݱ�
        //Save
        
        //���⿡ �����͸� �׼����� �̾����� �����͸� �������ִ� �ڵ� �ֱ�
        
        //����� ���� �ٽ� �ҷ����� ������ �������� ������� �ʴ°� ó�� ���δ�.
        //SceneManager.LoadScene("Shop");
    }
    public void SellRusultItem()
    {
        //���ӵ����Ϳ� ���� ���� �÷��ְ�
        //���Կ� �����ִ��� Ȯ���ϰ� ó��.
    }


}
