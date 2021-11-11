using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sgBullet : MonoBehaviour
{
   float damage = 10f; //ÃÑ¾Ë °ø°Ý·Â
   float speed = 5000f;//Åº ¼Ó

    Vector3 sgDir;

    public GameObject Bullet; //ÃÑ¾Ë ÇÁ¸®Æé »ç¿ë
    public GameObject firePos; //ÃÑ¾Ë ¹ß»ç À§Ä¡


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
