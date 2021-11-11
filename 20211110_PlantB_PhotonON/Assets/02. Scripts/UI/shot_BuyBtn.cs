using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_BuyMenu : MonoBehaviour
{

    Animator BoxAnim;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void BuyBox()
    {
        BoxAnim = GetComponent<Animator>();
        BoxAnim.SetTrigger("isBuy");
    }

}
