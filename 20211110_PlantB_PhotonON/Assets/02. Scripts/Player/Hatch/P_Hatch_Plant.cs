using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[포톤을 사용해야 하기때문에 니것 내것 경계 구분하기]
//>>pv.ismine

public class P_Hatch_Plant : MonoBehaviour, IGetSeed
{
    public bool isgetSeed;  //씨앗을 얻었는지 (최대 1개)
    public bool isOccupation;//땅의 점령 유무
    public GameObject OccEff; //점령 파티클생성
    PlayerInput playerInput;
    floor floorCheck;


    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        isgetSeed = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Stage" && isgetSeed == true )
        {   //콜리전 태그가 STAGE이고 씨앗을 얻은 상태일때 
            floorCheck = collision.gameObject.GetComponent<floor>();
            if (floorCheck.isOccu == false && playerInput.plant)
            {//점령 되지 않았고 플랜트버튼을 눌렀을때 
                //LandManager에서 FloorOcc[x,y]로 사용하기 위한 변수
                int x = floorCheck.floorNum % 10; 
                int y = floorCheck.floorNum / 10;
                Landmanager.instance.FloorOcc[x, y] = 1;// 기본값 = 0  1 = Player1 , 2 = Player2 ...
                Landmanager.instance.BingoCheck();//빙고 되었는지 체크 (가로 세로) + 대각선 추가해야함
                GameManager.instance.player1Score++;//플레이어 점령 수
                isgetSeed = false;//씨앗 얻은것 사라짐
                Instantiate(OccEff, collision.transform.position +new Vector3(0, -0.35f, 0),
                    Quaternion.identity);//플레이어별 바닥 생성
                floorCheck.isOccu = true;//점령됨
            }
        }
    }

    public void GetSeed()
    {
        isgetSeed = true;
    }
  
}
