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
   
}
