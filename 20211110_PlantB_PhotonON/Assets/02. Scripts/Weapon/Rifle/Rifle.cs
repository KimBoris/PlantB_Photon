using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Rifle : MonoBehaviour
{

    public enum State
    {
        Ready, //탄알이 준비됨
        Empty, //탄창이 빔
        Reloading //재장전중
    }

    public Transform firePos;//탄알이 발사될 위치
    public GameObject rifleBullet;
    public ParticleSystem muzzleFlashEff;//총구 화염 효과

    PhotonView pv;

    Image gunImage;
    Text gunStateText;
    public State state { get; private set; } //현재 상태
    PlayerInput PInput;

    ObjPooling objPooling;

    public string[] rifleBullets;//오브젝트 풀링에서 생성할 라이플 불렛(string)

    private AudioSource rifleAudio; //총 사운드 재생기
    public AudioClip shotClip; //발사 소리

    public float capacity = 20; //탄창 최대 총알수
    public float remainBullet = 0; //현재 총알 수


    public float lastFireTime; //마지막 총을 쏜 시점
    public float fireDelay = 0.25f;//총알 발사 간격
    public float reloadTime = 2.2f; //재장전 시간
    private void Awake()
    {
        gunStateText = GameObject.Find("GunState").GetComponent<Text>();
        pv = GetComponent<PhotonView>();
        muzzleFlashEff = firePos.Find("MuzzleFlashEffect").GetComponent<ParticleSystem>();
        gunImage = GameObject.Find("BulletsFill").GetComponent<Image>();
    }
    //private void Start()
    //{


    //}

    private void OnEnable()
    {
        remainBullet = capacity; //총알 가득 채우기
        state = State.Ready;
        lastFireTime = 0;
    }


    public void Fire()
    {//발사 시도 함수
     //State.Ready && 총알 발사간격이 맞다면
        if (state == State.Ready && Time.time >= lastFireTime + fireDelay)
        {
            lastFireTime = Time.time;
            //실제 발사
            Shot();
        }
    }

    [PunRPC]
    public void Shot()//실제 발사 함수
    {
        //총알 생성
        makeBullet();
        //Instantiate(rifleBullet, firePos.transform.position, firePos.rotation);

        muzzleFlashEff.Play();
        remainBullet--;

        if (remainBullet <= 0)
        {
            state = State.Empty;
        }

        pv.RPC("Shot", RpcTarget.Others, null);


        GunState();
    }

    public void Reload()
    {
        if (state == State.Reloading || remainBullet >= capacity)
        {//재장전 중, 현재 총알이 탄창최대수와 같거나 클때는 작동 X
            return;
        }
        StartCoroutine(ReloadRoutine());
        return;
    }
    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        yield return new WaitForSeconds(reloadTime);
        remainBullet = capacity;//총알 채우기

        state = State.Ready;//현재 상태 변경
    }
    private void makeBullet()
    {
        rifleBullet = objPooling.MakeObj(rifleBullets[0]);
        if (rifleBullet != null)
        {
            rifleBullet.transform.rotation = firePos.rotation;
            rifleBullet.transform.position = firePos.transform.position;
        }
    }
    void GunState() //현재 총알 갯수
    {
        gunImage.color = Color.yellow;
        Debug.Log(gunImage);
        gunImage.fillAmount = (remainBullet / capacity);
        gunStateText.text = string.Format("{0}", remainBullet);
        if (remainBullet < 11)
        {
            gunImage.color = Color.red;
        }
    }
}
