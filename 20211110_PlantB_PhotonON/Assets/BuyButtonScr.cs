using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButtonScr : MonoBehaviour
{
    public int itemNum;
    public string itemName;

    public Text ResultText;
    public Transform ResultPos;
    public GameObject ResultSet;//���â�� �������ִ� �������Ʈ
    public GameObject resultItem;
    public Image resultItemImg;

    void Start()
    {

    }

    void Update()
    {

    }
    public void BuyButtonDown() //���� �ִϸ��̼� ����
                                //1. ���� �̱� ����
                                //2. ������ �������� ���â�� ���°�
    {
        itemNum = Random.Range(1, 4);
        switch (itemNum)
        {

            case 1:
                itemName = "Armor";
                ResultText.text = "���� ���� ȹ��!";
                break;
            case 2:
                itemName = "Shoes";
                ResultText.text = "���� �Ź� ȹ��!";
                break;
            case 3:
                itemName = "Cloak";
                ResultText.text = "���� ���� ȹ��!";
                break;

        }
        ResultSet.SetActive(true);

        resultItem = Resources.Load<GameObject>("Accessory/" + itemName);
        resultItemImg.sprite = resultItem.GetComponent<ItemPickUp>().item.itemImage;

        //GameObject ResultItem = Resources.Load("Accessory/" + itemName) as GameObject;
        //ItemPrefab = Instantiate(ResultItem, ResultPos.position, Quaternion.identity);
        //GameObject gotcha = Resources.Load(itemName) as GameObject;
        //ItemPrefab = Instantiate(gotcha, transform.position, Quaternion.identity);
    }
}
