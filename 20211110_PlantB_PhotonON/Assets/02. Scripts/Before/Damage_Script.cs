using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Damage_Script : MonoBehaviour
{

    float iniHp = 100f; //초기 체력
    public float currHp; //현재 체력

    public Image hpBar;
    readonly Color initColor = new Vector4(0, 1f, 0, 1f); //체력바 초기 색상
    Color currColor; //체력바 현재 색상

    void Start()
    {
        currHp = iniHp;
        hpBar.color = initColor;
        currColor = initColor;
    }


    void DisplayHpbar()
    {
        if ((currHp / iniHp) > 0.5f)//현재 체력이 50보다 클때
        {
            currColor.r = (1 - (currHp / iniHp)) * 2.0f;
            //빨간색의 비율이 높아진다.
        }
        else //%까지는 노란색에서 빨간색으로
        {
            currColor.g = (currHp / iniHp) * 2f;
        }
        hpBar.color = currColor; //체력 게이지 색상 적용
        hpBar.fillAmount = (currHp / iniHp);//체력게이지
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Bullet")
       {//충돌한 물체의 태그 비교
            Destroy(other.gameObject);

            currHp -= 5; //체력 5감소

            DisplayHpbar(); //체력수지가 빠지고난후 체력바의 변화
       }
    }
    
}
