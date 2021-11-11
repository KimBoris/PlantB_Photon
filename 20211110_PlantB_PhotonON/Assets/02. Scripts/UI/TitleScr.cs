using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScr : MonoBehaviour
{
    public Image titleImg;

    void Start()
    {
        titleImg.fillAmount = 0;
    }

    void Update()
    {
        titleImg.fillAmount += Time.deltaTime*0.4f;
    }
}
