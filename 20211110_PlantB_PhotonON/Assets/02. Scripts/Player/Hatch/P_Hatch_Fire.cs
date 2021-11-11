using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Hatch_Fire : MonoBehaviour
{
    public GameObject gun;//รั
    private PlayerInput PInput;
    private Rifle rifle;



    void Awake()
    {
        PInput = GetComponent<PlayerInput>();
        rifle = GetComponentInChildren<Rifle>();
    }

    void Update()
    {
        if (PInput.fire)
        {
            rifle.Fire();
        }
        if (PInput.reload)
        {
            rifle.Reload();
        }
    }



}
