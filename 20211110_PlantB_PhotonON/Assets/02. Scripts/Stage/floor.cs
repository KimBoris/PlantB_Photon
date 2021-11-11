using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : MonoBehaviour
{
    //0으로 초기화 됨
    //바닥의 번호
    public int floorNum;


    // 0 0 0 0 1
    // 0 0 0 1 0
    // 0 0 1 0 0
    // 0 1 0 0 0
    // 1 2 0 0 0

    //fllorNum[,]

    public bool isOccu; //점령
    private void OnEnable()
    {
        isOccu = false;
    }
   
}
