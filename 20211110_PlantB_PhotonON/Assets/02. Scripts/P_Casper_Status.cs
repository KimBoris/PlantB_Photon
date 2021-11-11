using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class P_Casper_Status : MonoBehaviour, IDamage
{
    public float currHp; //���� ü��
    public float currMp; //���� ����
    public float maxHp;  //�ִ� ü��
    public float maxMp;  //�ִ� ����

    int damage; // �ϴ� �������� �س��� ���Ŀ� �Ѿ˿� ���� �����ؾ���

    float skillTime;//��ų ���� �ð�
    bool skillOn; //��ų ����

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
        //ĳ���� ��ų
        currMp = 0;
        //ĳ���� ��ų �߰�
        skillOn = true;
    }

    void SkillCoolTime()
    {//��ų ���� �ð�
        skillTime += Time.deltaTime;
        if (skillTime > 5)
        {
            skillTime = 0;
            skillOn = false;
            // ��ų �ٲٱ� casperMove.moveSpeed = 5;
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
