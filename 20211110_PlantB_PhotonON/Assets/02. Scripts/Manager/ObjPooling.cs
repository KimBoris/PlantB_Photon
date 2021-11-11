using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPooling : MonoBehaviour
{
    public GameObject rifleBulletPrefab;        //¶óÀÌÇÃ ÃÑ¾Ë
    //public GameObject shotgunBulletPrefab;    //¼¦°Ç ÃÑ¾Ë
    //public GameObject cannonBulletPrefab;     //Ä³³í ÃÑ¾Ë

    GameObject[] rifleBullet;               
    //GameObject[] shotgunBullet;
    //GameObject[] cannonBullet;

    GameObject[] targetPool;
    private void Awake()
    {
        rifleBullet = new GameObject[30];   //¶óÀÌÇÃ ÃÑ¾Ë
        //shotgunBullet = new GameObject[50]; //¼¦°Ç ÃÑ¾Ë
        //cannonBullet = new GameObject[10];  //Ä³³í ÃÑ¾Ë

        Generate();
    }

    void Generate()
    {
        //1. Bullet

        for (int index = 0; index < rifleBullet.Length; index++)
        {
            rifleBullet[index] = Instantiate(rifleBulletPrefab, transform.position, Quaternion.identity);
            rifleBullet[index].SetActive(false);
        }
        //for (int index = 0; index < shotgunBullet.Length; index++)
        //{
        //    shotgunBullet[index] = Instantiate(shotgunBulletPrefab);
        //    shotgunBullet[index].SetActive(false);
        //}
        //for (int index = 0; index < cannonBullet.Length; index++)
        //{
        //    cannonBullet[index] = Instantiate(cannonBulletPrefab);
        //    cannonBullet[index].SetActive(false);
        //}
    }
    public GameObject MakeObj(string type)
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            switch (type)
            {
                case "rifleBullet":
                    targetPool = rifleBullet;
                    break;
                //case "shotgunBullet":
                //    targetPool = shotgunBullet;
                //    break;
                //case "cannonBullet":
                //    targetPool = cannonBullet;
                //    break;

            }
            for (int index = 0; index < targetPool.Length; index++)
            {
                if (!targetPool[index].activeSelf)
                {
                    targetPool[index].SetActive(true);
                    return targetPool[index];
                }
            }
        }

        return null;
    }
}
