using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Casper_Plant : MonoBehaviour, IGetSeed
{
    public bool isgetSeed;
    public bool isOccupation;
    public GameObject OccEff2;
    //해치가  OccEff, 캐스퍼가 OccEff2, 가스너 OccEff3
    PlayerInput PInput;
    floor floorcheck;


    private void OnEnable()
    {
        PInput = GetComponent<PlayerInput>();
        isgetSeed = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Stage" && isgetSeed == true)
        {
            floorcheck = collision.gameObject.GetComponent<floor>();
            if (floorcheck.isOccu == false && PInput.plant)
            {
                int x = floorcheck.floorNum / 10;
                int y = floorcheck.floorNum % 10;
                Landmanager.instance.FloorOcc[x, y] = 2;//2번- 캐스퍼
                Landmanager.instance.BingoCheck();
                isgetSeed = false;
                Instantiate(OccEff2, collision.transform.position + new Vector3(0, 0.35f, 0), Quaternion.identity);
            }
        }
    }

    public void GetSeed()
    {
        isgetSeed = true;
    }
}
