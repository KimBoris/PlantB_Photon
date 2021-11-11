using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmanager : MonoBehaviour
{

    public static Landmanager instance = null;

    public int BingoCount;

    public int[,] FloorOcc = new int[5, 5];//�� �ٴڸ��� ��ȣ ����

    private void OnEnable()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    public void BingoCheck()
    {
        //���� üũ
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                if (FloorOcc[j, i] == 1)
                {
                    BingoCount++;
                }
            }
            if (BingoCount >= 5)
            {
                GameManager.instance.isGameover = true;
                GameManager.instance.GameOver();
                BingoCount = 0;
                return;
            }
            else
            {
                BingoCount = 0;
            }
        }
        //����üũ
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (FloorOcc[j, i] == 1)
                {
                    BingoCount++;
                }
            }
            if (BingoCount >= 5)
            {
                GameManager.instance.isGameover = true;
                GameManager.instance.GameOver();
                BingoCount = 0;

                return;
            }
            else
            {
                BingoCount = 0;
            }
        }
        //�밢�� üũ
        for (int i = 0; i < 5; i++)
        {
            if (FloorOcc[i, i] == 1)
            {
                BingoCount++;
            }
            if (BingoCount >= 5)
            {
                GameManager.instance.GameOver();
                return;
            }
            else
            {
                BingoCount = 0;
            }
        }

        //�� ��ȣ�� � �÷��̾��� �÷�Ʈ�� �Ǿ��ִ��� üũ�ϴ� ���.


        //public static Landmanager instance;
        //public enum LandHost
        //{
        //    none,   //������ ���� ����
        //    player1,
        //    player2,
        //    player3,
        //    player4
        //}

        //public LandHost landHost;


        //private void Awake()
        //{
        //    landHost = LandHost.none;
        //   // landHost = LandHost.none;
        //    if (instance == null)
        //    {
        //        instance = this;
        //    }
        //    else if (instance != this)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}

        //private void Update()
        //{

        //}


    }
}
