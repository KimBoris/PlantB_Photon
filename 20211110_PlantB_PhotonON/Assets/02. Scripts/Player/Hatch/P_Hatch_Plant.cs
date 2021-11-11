using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[������ ����ؾ� �ϱ⶧���� �ϰ� ���� ��� �����ϱ�]
//>>pv.ismine

public class P_Hatch_Plant : MonoBehaviour, IGetSeed
{
    public bool isgetSeed;  //������ ������� (�ִ� 1��)
    public bool isOccupation;//���� ���� ����
    public GameObject OccEff; //���� ��ƼŬ����
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
        {   //�ݸ��� �±װ� STAGE�̰� ������ ���� �����϶� 
            floorCheck = collision.gameObject.GetComponent<floor>();
            if (floorCheck.isOccu == false && playerInput.plant)
            {//���� ���� �ʾҰ� �÷�Ʈ��ư�� �������� 
                //LandManager���� FloorOcc[x,y]�� ����ϱ� ���� ����
                int x = floorCheck.floorNum % 10; 
                int y = floorCheck.floorNum / 10;
                Landmanager.instance.FloorOcc[x, y] = 1;// �⺻�� = 0  1 = Player1 , 2 = Player2 ...
                Landmanager.instance.BingoCheck();//���� �Ǿ����� üũ (���� ����) + �밢�� �߰��ؾ���
                GameManager.instance.player1Score++;//�÷��̾� ���� ��
                isgetSeed = false;//���� ������ �����
                Instantiate(OccEff, collision.transform.position +new Vector3(0, -0.35f, 0),
                    Quaternion.identity);//�÷��̾ �ٴ� ����
                floorCheck.isOccu = true;//���ɵ�
            }
        }
    }

    public void GetSeed()
    {
        isgetSeed = true;
    }
  
}
