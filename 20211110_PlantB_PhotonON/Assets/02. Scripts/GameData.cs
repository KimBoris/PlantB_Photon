using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameData
{

    public int UserCoin = 0;  //유저의 돈
    public int UserRuby = 0;  //유저의 루비
    public int UserLevel = 1; //유저의 레벨
    public float UserExp = 0;   //유저의 경험치
    public string UserCharacter; //유저가 선택했던 캐릭터
    public string UserId;        //유저 ID
    public string UserPw;        //유저 PW

    public List<string> haveItemName = new List<string>(); //유저의 상점 인벤토리에 있는 아이템
    //public List<string> equipItemName = new List<string>() { "null", "null", "null"}; //유저의 장착 슬롯에 있는 아이템
    //public string[] equipItemName = new string[3] { "","",""};
    public string equiItemName0;
    public string equiItemName1;
    public string equiItemName2;
    //[보류]
    //public int UserAccUpgrade;//유저 악세사리 업그레이드

    void Start()
    {
   
    }

    void Update()
    {

    }
}
