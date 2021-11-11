using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //�÷��̾� ĳ���͸� �����ϱ� ���� �Է� ����
    //������ �Է°��� �ٸ� ������Ʈ�� ����� �� �ֵ��� ����
    public string moveVerticalName = "Vertical";     //���� �̵�
    public string moveHorizontalName = "Horizontal"; //�¿� �̵�
    public string rotateName = "Mouse X";            //�÷��̾� ȸ��
    public string fireButtonName = "Fire1";          //�߻�
    public string reloadButtonName = "Reload";       //������
    public string jumpButtonName = "Jump";           //����

    public float moveV { get; private set; }    //���� �̵���    
    public float moveH { get; private set; }    //�¿� �̵���
    public float rotate { get; private set; }   //ȸ����
    public bool fire { get; private set; }      //�߻� �Է�
    public bool reload { get; private set; }    //������ �Է�
    public bool skill { get; private set; }     //��ų
    public bool plant { get; private set; }     //�÷�Ʈ
    void Update()
    {
        //���� ���� ���¿����� ����� �Է��� ���� ���� �ʰ�
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
        
            //move�� ���� �Է� ����
            moveV = Input.GetAxis(moveVerticalName);
            moveH = Input.GetAxis(moveHorizontalName);
            //rotate�� ���� �Է� ����
            rotate = Input.GetAxis(rotateName);
            //�߻翡 ���� �Է� ����
            fire = Input.GetButton(fireButtonName);
            //������
            reload = Input.GetKeyDown(KeyCode.R);
            //��ų���
            skill = Input.GetMouseButtonDown(1);
            //�÷�Ʈ
            plant = Input.GetKeyDown(KeyCode.T);
    }
}
