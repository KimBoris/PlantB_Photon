using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rifle : MonoBehaviour
{

    int damageRifle; //�Ѿ� ���ݷ�

    float speed;//�Ѿ� �ӵ�
    float bulletOff; //�Ѿ� ���°�

    Vector3 dir; //�Ѿ� ����

    TrailRenderer trail;

    public GameObject destroyEff;

    private void OnEnable()
    {
        trail = GetComponent<TrailRenderer>();
        damageRifle = 12;
        dir = new Vector3(0,0,0.2f);
        speed = 1000f;
        destroyEff = Resources.Load("Rifle_EndEff") as GameObject;
    }

    void Update()
    {
        BulletMove();
    }

    private void OnTriggerEnter(Collider other)
    {

        IDamage damage = other.GetComponent<IDamage>();

        if (other.tag == "Wall"||destroyEff != null)
        {
            destroyEff = Instantiate(destroyEff, this.transform.position, Quaternion.identity);
            Destroy(destroyEff, 1f);
            gameObject.SetActive(false);

        }
        else if(damage != null && other.tag == "Player")
        {
            damage = other.gameObject.GetComponent<IDamage>();
            damage.Damage(damageRifle);
            this.gameObject.SetActive(false);
        }
        trail.Clear();
    }

    void BulletMove()
    {
        bulletOff += Time.deltaTime;
        if (bulletOff > 3)
        {
            bulletOff = 0;
            gameObject.SetActive(false);

        }
        this.transform.Translate(dir* speed * Time.deltaTime);
    }
}
