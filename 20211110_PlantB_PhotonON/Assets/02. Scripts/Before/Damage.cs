using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject shotgunBullet;
    public GameObject canonBullet;
    public GameObject rifleBullet;
    float PlayerHp;
    void Start()
    {
        PlayerHp = GetComponent<Hatch_Player_Script>().currHp;
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            PlayerHp-= 12;
        }
        else if(other.tag == "CanonBullet")
        {
            PlayerHp -= 150;
        }
        else if(other.tag == "sgBullet")
        {
            PlayerHp -= 60;
        }
    }
}

