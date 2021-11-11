using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class P_Hatch_Shot : MonoBehaviour
//격발, 발사, UI체크까지
{

    //[입력]
    PlayerInput playerInput;

    //오브젝트 풀링[총알]
    ObjPooling objPooling;

    string[] rifleBullets; //오브젝트 풀링에서 생성할 라이플 불렛(string)//<<

    //[총]
    public enum State//총의 현재 상태
    {
        Ready, //탄알, 격발 준비됨
        Empty, //탄창이 빔
        Reloading //재장전중
    }
    public State state { get; private set; }//총의 현재상태

    public Transform firePos; //총알이 발사될 위치
    public ParticleSystem muzzleFlashEff; //총구의 화염 효과
    GameObject rifleBullet; //총알

    float capacity = 20; //탄창 최대 총알 수
    float remainBullet = 0;//현재 총알 수


    //[총 관련 UI]
    Image bulletFillImg;   //총알의 남은 갯수 이미지
    Text bulletLeftText;//총알의 현재 개수

    //[사운드]
    private AudioSource rifleAudio; //총의 사운드 재생기
    AudioClip shotClip;//총 발사 소리

    //[발사]
    float lastFireTime; //마지막으로 총을 쏜 시점
    float fireDelay = 0.25f;//총알 발사 간격
    float reloadTime = 2.2f;//장전 시간

    //성능향상을 위해 캐싱하는 방법
    bool isMouseClick => Input.GetMouseButton(0); //마우스 좌측키 클릭
    bool isReloadButton => Input.GetKeyDown(KeyCode.R);//R키 

    //[포톤]
    PhotonView pv;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        playerInput = GetComponent<PlayerInput>();
        objPooling = GameObject.Find("ObjectPooling").GetComponent<ObjPooling>();
        rifleBullets = new string[] { "rifleBullet" }; //오브젝트 풀링에서 사용될 이름
        bulletFillImg = GameObject.Find("BulletFill(Image)").GetComponent<Image>();
        bulletLeftText = GameObject.Find("BulletLeftText(Image)").GetComponent<Text>();

        //remainBullet = capacity; //총알 가득 채워넣어주기
        //state = State.Ready; //총의 현재 상태
        //lastFireTime = 0; //마지막으로 총 발사시점 0
    }
    private void OnEnable()
    {
        remainBullet = capacity;    //아이템 착용시 값을 변경해주기 위해서 OnEnable에 넣음
        lastFireTime = 0; //마지막으로 총 발사시점 0
        state = State.Ready;

        //아이템 장착시 능력치 값을 변경해주는 함수 넣어주기
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            RelateShotBtn();//버튼 입력을 처리하는 함수 격발, 리로드
        }
    }
    void Fire()
    {//발사를 '시도' = 바로 쏘는것이 아니라 마우스 클릭을 감지한다
        if (state == State.Ready && Time.time > lastFireTime + fireDelay)
        {//총의 상태, (마지막 쏜 시점+발사 딜레이) 확인
            lastFireTime = Time.time; //마지막 총을 쏜 시점에 Time.time을 넣어주어 초기화

            //실제 발사되는 함수 넣어주기
            Shot();
        }
    }
    [PunRPC]
    void Shot()
    {//실제로 발사 시키는 함수

        makeBullet(); //생성되어있던 총알 불러오기

        muzzleFlashEff.Play();//머즐플레시를 재생시킨다.(총구화염)
        remainBullet--;
        if (remainBullet <= 0)
        {
            state = State.Empty;
        }

        pv.RPC("Shot", RpcTarget.Others, null);
       
    }

    
    void Reload()
    {
        if (state == State.Reloading || remainBullet >= capacity)
        {//리로딩 중이거나, 총알이 탄창수와 을때 작동 X(20/20)
            return;
        }
        StartCoroutine(ReloadRoutine());//실제 장전하는 함수

      
        return;

    }

    IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        yield return new WaitForSeconds(reloadTime);
        remainBullet = capacity; //총알 장전

        state = State.Ready; //현재상태를 Ready로 바꿔준다.
    }

    void makeBullet()
    {//오브젝트 풀링에서 생성된 총알을 실제로 사용하는 함수
        rifleBullet = objPooling.MakeObj(rifleBullets[0]);
        //ObjPooling.MakeObj함수에 rifleBullets[0]매개변수 넣어주기
        if (rifleBullet != null)
        {
            rifleBullet.transform.position = firePos.position;//총알의 위치를 FirePos
            rifleBullet.transform.rotation = firePos.rotation;//총알의 방향을 FirePos
        }
    }

    void GunState()
    {//현재 총알수를 가지고 발사시 UI체크
        bulletFillImg.color = Color.yellow;//총알 갯수 나타내는 UI = 노란색 Fill
        bulletFillImg.fillAmount = (remainBullet / capacity);
        bulletLeftText.text = string.Format("{0}", remainBullet);//remainBullet을 text로 나타낸다
        if (remainBullet < 11)//총알이 11보다 작으면
        {
            bulletFillImg.color = Color.red;
        }
    }

    void RelateShotBtn()//총과 관련된 버튼 입력시
    {
        if (isMouseClick)
        {
            Fire();
        }
        else if (isReloadButton)
        {
            Reload();
        }
        //현재 총알을 나타내는 UI 체크 함수넣어주기
        GunState();
    }
}
