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
    public string[] shotgunBullets; //오브젝트 풀링에서 사용할 불렛

    public Transform firePos;//탄알이 발사될 위치
    public ParticleSystem muzzleFlashEff; //총구 화염 효과

    private AudioSource shotGunAudio; //총 사운드 재생기
    public AudioClip shotClip;//발사소리 

    public float capacity = 5; //탄창 최대 총알 
    public float remainBullet = 0; //현재 총알 수

    public float lastFireTime;
    public float fireDelay = 1f;
    public float reloadTime = 0.8f; //한발에 0.8초


    private void OnEnable()
    {
        PInput = GetComponentInParent<PlayerInput>();
        shotGunAudio = GetComponent<AudioSource>();
        objPooling = GameObject.Find("ObjectPooling").GetComponent<ObjPooling>();

        shotgunBullets = new string[] { "shotgunBullet" };
        remainBullet = capacity; //등장시 총알을 가득 채운다
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
