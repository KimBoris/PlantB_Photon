using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameData
{

    public int UserCoin = 0;  //������ ��
    public int UserRuby = 0;  //������ ���
    public int UserLevel = 1; //������ ����
    public float UserExp = 0;   //������ ����ġ
    public string UserCharacter; //������ �����ߴ� ĳ����
    public string UserId;        //���� ID
    public string UserPw;        //���� PW

    public List<string> haveItemName = new List<string>(); //������ ���� �κ��丮�� �ִ� ������
    //public List<string> equipItemName = new List<string>() { "null", "null", "null"}; //������ ���� ���Կ� �ִ� ������
    //public string[] equipItemName = new string[3] { "","",""};
    public string equiItemName0;
    public string equiItemName1;
    public string equiItemName2;
    //[����]
    //public int UserAccUpgrade;//���� �Ǽ��縮 ���׷��̵�

    void Start()
    {
   
    }

    void Update()
    {

    }
}
