using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : MonoBehaviour
{
    //0���� �ʱ�ȭ ��
    //�ٴ��� ��ȣ

    public int floorNum;

    // 0 0 0 0 1
    // 0 0 0 1 0
    // 0 0 1 0 0
    // 0 1 0 0 0
    // 1 2 0 0 0

    //fllorNum[,]

    public bool isOccu; //����
    private void OnEnable()
    {
        isOccu = false;
    }







    //public enum LandHost
    //{
    //    none,   //������ ���� ����
    //    player1,
    //    player2,
    //    player3,
    //    player4
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name == "RedFloor(Clone)")
    //    {
    //        landhost = LandHost.player1;
    //        Landmanager.instance.Player1Count++;
    //        Debug.Log(Landmanager.instance.Player1Count);
    //        Debug.Log(this.gameObject.name);
    //    }
    //    else if (other.name == "BlueFloor(Clone)")
    //    {
    //        landhost = LandHost.player2;
    //        Landmanager.instance.Player2Count++;
    //        Debug.Log(Landmanager.instance.Player2Count);

    //    }
    //    else if (other.name == "GreenFloor(Clone)")
    //    {
    //        landhost = LandHost.player3;
    //        Landmanager.instance.Player3Count++;
    //        Debug.Log(Landmanager.instance.Player3Count);


    //    }
    //    else if (other.name == "YellowFloor(Clone)")
    //    {
    //        landhost = LandHost.player4;
    //        Landmanager.instance.Player4Count++;
    //        Debug.Log(Landmanager.instance.Player4Count);
    //    }
    //}
}
