using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class P_Hatch_Status : MonoBehaviour, IDamage
{
    Renderer[] renderers; //사망 후 투명 처리를 위해서 meshRenderer

    public float currHp; //현재 체력
    public float currMp; //현재 마나
    public float maxHp;  //최대 체력
    public float maxMp;  //최대 마나

    int damage; // 일단 데미지로 해놓고 추후에 총알에 따라서 수정해야함

    float skillTime;//스킬 시전 시간
    bool skillOn; //스킬 시전

    public bool isDie;

    P_Hatch_Move hatchMove;
    PlayerInput playerInput;
    Animator HatchAnim;

    string[] EquipItemName; //장착중인 아이템

    string EquipSlotItem0;
    string EquipSlotItem1;
    string EquipSlotItem2;

    //[스킬]
    public float mpUpSpeed;

    private void OnEnable()
    {
        //생성시 장착중인 아이템 적용
        playerInput = GetComponent<PlayerInput>();
        HatchAnim = GetComponent<Animator>();
        hatchMove = GetComponent<P_Hatch_Move>();
        maxHp = 80;
        maxMp = 100;

        currMp = 0;
        mpUpSpeed = 2;

        isDie = false;
        EquipItemName = new string[3] { GameDataManager.instance.gameData.equiItemName0, GameDataManager.instance.gameData.equiItemName1, GameDataManager.instance.gameData.equiItemName2 };
        EquipItemSlot();//아이템에 따라 적용해줄 능력치
        currHp = maxHp;
    }

    void Update()
    {
        //마나 증가 (초당 2씩)
        currMp += mpUpSpeed * Time.deltaTime;
        if (currMp > maxMp)
        {
            currMp = maxMp;
            if (playerInput.skill)
            {
                HatchSkill();
            }
        }

        //스킬 사용 가능 (마나 100)
        if (skillOn == true)
        {
            SkillCoolTime();
        }

    }
    void HatchSkill()
    {//해치 스킬 : 이동속도 증가
        currMp = 0;
        hatchMove.moveSpeed = 10;
        skillOn = true;
    }
    void SkillCoolTime()
    {//스킬 지속 시간
        skillTime += Time.deltaTime;
        if (skillTime > 5)
        {
            skillTime = 0;
            skillOn = false;
            hatchMove.moveSpeed = 5;
        }
    }



    void IDamage.Damage(int damage)
    {
        currHp -= damage;
        if (currHp <= 0)
        {//사망처리
            StartCoroutine(PlayerDie());
        }
    }

    IEnumerator PlayerDie()
    {
        HatchAnim.SetTrigger("isDie");//플레이어 애니메이션 다이
        playerInput.enabled = false;//플레이어 조작 끄기
        yield return new WaitForSeconds(3f);

        SetPlayerVisible(false);//투명 처리 함수 호출

        yield return new WaitForSeconds(1.5f);

        currHp = maxHp;

        SetPlayerVisible(true);

        playerInput.enabled = true;
    }
    void SetPlayerVisible(bool isVisible)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = isVisible;
        }
    }

    void EquipItemSlot() // 아이템을 끼고있으면 능력치 상승
    {
        for (int i = 0; i < EquipItemName.Length; i++)
        {
            if (EquipItemName[i] == "Armor")
            {
                maxHp *= 1.3f;

            }
            else if (EquipItemName[i] == "Shoes")
            {
                hatchMove.moveSpeed += 3;

            }
            else if (EquipItemName[i] == "Cloak")
            {
                mpUpSpeed += 2;

            }
        }

    }

}
