using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P_Casper_Status : MonoBehaviour, IDamage
{
    public float currHp; //현재 체력
    public float currMp; //현재 마나
    public float maxHp;  //최대 체력
    public float maxMp;  //최대 마나

    int damage; // 일단 데미지로 해놓고 추후에 총알에 따라서 수정해야함

    float skillTime;//스킬 시전 시간
    bool skillOn; //스킬 시전

    public bool isDie;

    P_Casper_Move casperMove;
    PlayerInput playerInput;
    Animator CasperAnim;

    public Image hpBar;
    public Image mpBar;


    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        casperMove = GetComponent<P_Casper_Move>();
        CasperAnim = GetComponent<Animator>();
        currHp = 100;
        currMp = 0;
        maxHp = 100;
        maxMp = 100;

        isDie = false;
    }
    void Update()
    {
        currMp += Time.deltaTime;
        if (currMp > maxMp)
        {
            currMp = maxMp;
            if (playerInput.skill)
            {
                CasperSkill();
            }
        }

        if (skillOn == true)
        {
            SkillCoolTime();
        }
    }

    void CasperSkill()
    {
        //캐스퍼 스킬
        currMp = 0;
        //캐스퍼 스킬 추가
        skillOn = true;
    }

    void SkillCoolTime()
    {//스킬 지속 시간
        skillTime += Time.deltaTime;
        if (skillTime > 5)
        {
            skillTime = 0;
            skillOn = false;
            // 스킬 바꾸기 casperMove.moveSpeed = 5;
        }
    }
 void IDamage.Damage(int damage)
    {
        currHp -= damage;
        if (currHp <= 0)
        {
            CasperAnim.SetTrigger("isDie");
            isDie = true;
        }
    }
}
