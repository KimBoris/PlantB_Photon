using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Casper_Fire : MonoBehaviour
{

    public GameObject gun;//รั

    private PlayerInput PInput;

    //private Shotgun shotgun;

    void Awake()
    {
        PInput = GetComponent<PlayerInput>();
        //shotgun = GetComponentInChildren<Shotgun>();
    }


    void Update()
    {
        if (PInput.fire)
        {
            //shotgun.Fire();
        }
        if (PInput.reload)
        {
            //shotgun.Reload();
        }
    }

}
