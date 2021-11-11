using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Damage_Script : MonoBehaviour
{

    float iniHp = 100f; //�ʱ� ü��
    public float currHp; //���� ü��

    public Image hpBar;
    readonly Color initColor = new Vector4(0, 1f, 0, 1f); //ü�¹� �ʱ� ����
    Color currColor; //ü�¹� ���� ����

    void Start()
    {
        currHp = iniHp;
        hpBar.color = initColor;
        currColor = initColor;
    }


    void DisplayHpbar()
    {
        if ((currHp / iniHp) > 0.5f)//���� ü���� 50���� Ŭ��
        {
            currColor.r = (1 - (currHp / iniHp)) * 2.0f;
            //�������� ������ ��������.
        }
        else //%������ ��������� ����������
        {
            currColor.g = (currHp / iniHp) * 2f;
        }
        hpBar.color = currColor; //ü�� ������ ���� ����
        hpBar.fillAmount = (currHp / iniHp);//ü�°�����
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Bullet")
       {//�浹�� ��ü�� �±� ��
            Destroy(other.gameObject);

            currHp -= 5; //ü�� 5����

            DisplayHpbar(); //ü�¼����� �������� ü�¹��� ��ȭ
       }
    }
    
}
