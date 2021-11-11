using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmanager : MonoBehaviour
{

    public static Landmanager instance = null;

    public int BingoCount;

    public int[,] FloorOcc = new int[5, 5];//각 바닥마다 번호 설정

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
        //세로 체크
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
        //가로체크
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
        //대각선 체크
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

        //땅 번호에 어떤 플레이어의 플랜트가 되어있는지 체크하는 방법.


        //public static Landmanager instance;
        //public enum LandHost
        //{
        //    none,   //주인이 없는 상태
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
