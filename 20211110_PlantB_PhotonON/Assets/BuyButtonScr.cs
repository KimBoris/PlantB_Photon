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
    public GameObject ResultSet;//°á°úÃ¢À» °¡Áö°íÀÖ´Â ºó¿ÀºêÁ§Æ®
    public GameObject resultItem;
    public Image resultItemImg;

    void Start()
    {

    }

    void Update()
    {

    }
    public void BuyButtonDown() //»óÀÚ ¾Ö´Ï¸ÞÀÌ¼Ç ÀÌÈÄ
                                //1. ·£´ý »Ì±â ÁøÇà
                                //2. ¾ÆÀÌÅÛ ÇÁ¸®ÆéÀ» °á°úÃ¢¿¡ ¶ç¿ì´Â°Í
    {
        itemNum = Random.Range(1, 4);
        switch (itemNum)
        {

            case 1:
                itemName = "Armor";
                ResultText.text = "°¡Á× °©¿Ê È¹µæ!";
                break;
            case 2:
                itemName = "Shoes";
                ResultText.text = "°¡Á× ½Å¹ß È¹µæ!";
                break;
            case 3:
                itemName = "Cloak";
                ResultText.text = "°¡Á× ¸ÁÅä È¹µæ!";
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
