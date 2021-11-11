using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_col_Script : MonoBehaviour
{

    public GameObject Flag; // ¾¾
    public GameObject Floor;//¾¾¾Ñ ½ÉÀ¸¸é ¹Ù²î´Â »ö»ó ¹Ù´Ú
    Material mt;
    Transform tf;
    void Start()
    {
        mt = GetComponent<Material>();
    }

    void Update()
    {
         mt.color = Color.red;
    }

 


}
