using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class P_Hatch_Status : MonoBehaviour, IDamage
{
    Renderer[] renderers; //��� �� ���� ó���� ���ؼ� meshRenderer

    public float currHp; //���� ü��
    public float currMp; //���� ����
    public float maxHp;  //�ִ� ü��
    public float maxMp;  //�ִ� ����

    int damage; // �ϴ� �������� �س��� ���Ŀ� �Ѿ˿� ���� �����ؾ���

    float skillTime;//��ų ���� �ð�
    bool skillOn; //��ų ����

    public bool isDie;

    P_Hatch_Move hatchMove;
    PlayerInput playerInput;
    Animator HatchAnim;

    string[] EquipItemName; //�������� ������

    string EquipSlotItem0;
    string EquipSlotItem1;
    string EquipSlotItem2;

    //[��ų]
    public float mpUpSpeed;

    private void OnEnable()
    {
        //������ �������� ������ ����
        playerInput = GetComponent<PlayerInput>();
        HatchAnim = GetComponent<Animator>();
        hatchMove = GetComponent<P_Hatch_Move>();
        maxHp = 80;
        maxMp = 100;

        currMp = 0;
        mpUpSpeed = 2;

        isDie = false;
        EquipItemName = new string[3] { GameDataManager.instance.gameData.equiItemName0, GameDataManager.instance.gameData.equiItemName1, GameDataManager.instance.gameData.equiItemName2 };
        EquipItemSlot();//�����ۿ� ���� �������� �ɷ�ġ
        currHp = maxHp;
    }

    void Update()
    {
        //���� ���� (�ʴ� 2��)
        currMp += mpUpSpeed * Time.deltaTime;
        if (currMp > maxMp)
        {
            currMp = maxMp;
            if (playerInput.skill)
            {
                HatchSkill();
            }
        }

        //��ų ��� ���� (���� 100)
        if (skillOn == true)
        {
            SkillCoolTime();
        }

    }
    void HatchSkill()
    {//��ġ ��ų : �̵��ӵ� ����
        currMp = 0;
        hatchMove.moveSpeed = 10;
        skillOn = true;
    }
    void SkillCoolTime()
    {//��ų ���� �ð�
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
        {//���ó��
            StartCoroutine(PlayerDie());
        }
    }

    IEnumerator PlayerDie()
    {
        HatchAnim.SetTrigger("isDie");//�÷��̾� �ִϸ��̼� ����
        playerInput.enabled = false;//�÷��̾� ���� ����
        yield return new WaitForSeconds(3f);

        SetPlayerVisible(false);//���� ó�� �Լ� ȣ��

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

    void EquipItemSlot() // �������� ���������� �ɷ�ġ ���
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
