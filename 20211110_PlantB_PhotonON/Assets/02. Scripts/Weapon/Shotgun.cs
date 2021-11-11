using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{

    public enum State
    {
        Ready, 
        Empty,
        Reloading
    }
    public State state { get; private set; }
    PlayerInput PInput;

    ObjPooling objPooling;

    public GameObject shotgunBullet;
    public string[] shotgunBullets; //������Ʈ Ǯ������ ����� �ҷ�

    public Transform firePos;//ź���� �߻�� ��ġ
    public ParticleSystem muzzleFlashEff; //�ѱ� ȭ�� ȿ��

    private AudioSource shotGunAudio; //�� ���� �����
    public AudioClip shotClip;//�߻�Ҹ� 

    public float capacity = 5; //źâ �ִ� �Ѿ� 
    public float remainBullet = 0; //���� �Ѿ� ��

    public float lastFireTime;
    public float fireDelay = 1f;
    public float reloadTime = 0.8f; //�ѹ߿� 0.8��


    private void OnEnable()
    {
        PInput = GetComponentInParent<PlayerInput>();
        shotGunAudio = GetComponent<AudioSource>();
        objPooling = GameObject.Find("ObjectPooling").GetComponent<ObjPooling>();

        shotgunBullets = new string[] { "shotgunBullet" };
        remainBullet = capacity; //����� �Ѿ��� ���� ä���
        state = State.Ready;
        lastFireTime = 0;
    }
    void Update()
    {
        
    }
    public void Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + fireDelay)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }
    public void Shot()
    {
        Instantiate(shotgunBullet, firePos.transform.position, firePos.rotation);
    }


}
