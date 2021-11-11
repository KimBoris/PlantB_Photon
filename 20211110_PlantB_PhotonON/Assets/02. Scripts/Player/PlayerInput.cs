using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //플레이어 캐릭터를 조작하기 위한 입력 감지
    //감지된 입력값을 다른 컴포넌트가 사용할 수 있도록 제공
    public string moveVerticalName = "Vertical";     //상하 이동
    public string moveHorizontalName = "Horizontal"; //좌우 이동
    public string rotateName = "Mouse X";            //플레이어 회전
    public string fireButtonName = "Fire1";          //발사
    public string reloadButtonName = "Reload";       //재장전
    public string jumpButtonName = "Jump";           //점프

    public float moveV { get; private set; }    //상하 이동값    
    public float moveH { get; private set; }    //좌우 이동값
    public float rotate { get; private set; }   //회전값
    public bool fire { get; private set; }      //발사 입력
    public bool reload { get; private set; }    //재장전 입력
    public bool skill { get; private set; }     //스킬
    public bool plant { get; private set; }     //플랜트
    void Update()
    {
        //게임 오버 상태에서는 사용자 입력을 감지 하지 않게
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            moveH = 0;
            moveV = 0;
            rotate = 0;
            fire = false;
            reload = false;
            plant = false;
            skill = false;
            return;
        }
        
            //move에 관한 입력 감지
            moveV = Input.GetAxis(moveVerticalName);
            moveH = Input.GetAxis(moveHorizontalName);
            //rotate에 관한 입력 감지
            rotate = Input.GetAxis(rotateName);
            //발사에 관한 입력 감지
            fire = Input.GetButton(fireButtonName);
            //재장전
            reload = Input.GetKeyDown(KeyCode.R);
            //스킬사용
            skill = Input.GetMouseButtonDown(1);
            //플랜트
            plant = Input.GetKeyDown(KeyCode.T);
    }
}
