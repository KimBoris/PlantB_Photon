using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sgBullet : MonoBehaviour
{
   float damage = 10f; //�Ѿ� ���ݷ�
   float speed = 5000f;//ź ��

    Vector3 sgDir;

    public GameObject Bullet; //�Ѿ� ������ ���
    public GameObject firePos; //�Ѿ� �߻� ��ġ


    Rigidbody rb;
    Transform tr;
    TrailRenderer trail;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        trail = GetComponent<TrailRenderer>();  
    }

    void Update()
    {

    }

}
