using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_col_Script : MonoBehaviour
{

    public GameObject Flag; // ��
    public GameObject Floor;//���� ������ �ٲ�� ���� �ٴ�
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
