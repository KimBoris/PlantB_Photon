using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hatch_Player_Script : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    float moveX; //가로
    float moveZ; //세로
    float rotateY; //가로회전
    float rotateX; //세로회전

    float moveSpeed; //이동 속도
    float rotSpeed;  //회전 속도
    float jumpPower; //점프파워

    public float currHp; //현재 체력
    public float currMp; //현재 마나
    float maxHp; // 최대 체력
    float maxMp; // 최대 마나

    float skillTime;//스킬 시전 시간

    float spawnAreaTime;//스폰지역에 머무른 시간

    Vector3 Dir; //플레이어 이동 방향

    Transform tr;
    Rigidbody rb;

    bool isMove;//애니메이션 적용시
    bool isDie; //플레이어 사망
    bool isSkillOn; //스킬 사용 가능
    bool isGetSeed; //씨앗 획득 여부      (참 : 가능, 거짓 : 불가능)
    bool isAblePlant; //씨앗 심기 불/가능 여부 ( 참 : 가능, 거짓 : 불가능 )
    bool isGetFloor; //땅 점령 유무        
    bool isJump; //점프 유무

    public GameObject PlantArea; //씨앗 심은 구역
    public GameObject eff_Seed;  //씨앗 획득시 파티클
    public Image hpBar;  //체력바
    public Image mpBar;  //마나바

    public GameObject[] rifleBullet;  //라이플 총알
    public GameObject sgBullet;     //샷건 총알
    public GameObject canonBullet;  //대표 총알
    public GameObject redFloor; //씨앗 심으면 플레이어 색상으로 바뀌는 바닥
    public float plantCount; //점령한 땅갯수

    void Start()
    {

        moveSpeed = 10f;
        rotSpeed = 400f;
        jumpPower = 260f;

        currHp = 80;
        currMp = 0;
        maxHp = 80;
        maxMp = 100;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isJump = false;
        isSkillOn = false;
        isDie = false;
        isGetSeed = false;
        isAblePlant = false;
        isGetFloor = false;
        plantCount = 0;

    }

    void Update()
    {
        if (isDie == false)
        {
            //[플레이어 이동]
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            rotateY = Input.GetAxis("Mouse X");
            rotateX = Input.GetAxis("Mouse Y");

            Dir = new Vector3(moveX, 0f, moveZ);
            Dir = Dir.normalized;

            tr.Translate(Dir * Time.deltaTime * moveSpeed, Space.Self); //플레이어 방향
            tr.Rotate(new Vector3(0, rotateY, 0) * Time.deltaTime * rotSpeed); //플레이어 회전(가로)

            //[플레이어 점프]
            if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Force);
                isJump = true;
            }

            //[애니메이션]
            isMove = Dir.magnitude != 0;                   //플레이어 이동벡터값 ( 애니메이션 구별을 위해서 )
            animator.SetFloat("moveSpeed", Dir.magnitude); //아바타 마스크 (상체 - 슛, 하체 - 달리기, 정지)

            //Hp, Mp Bar

            //[플레이어 마나]
            if (currMp <= 100)
            {
                currMp += Time.deltaTime;
                if (currMp > 100)
                {
                    currMp = 100;
                }
            }
            //[플레이어 스킬] - 행진 : 5초간 이동속도 2배 증가
            if (currMp >= 100)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    moveSpeed = 10f;
                    Debug.Log("스킬사용!!");
                    currMp -= 100;
                    isSkillOn = false;
                }
            }
            //[플레이어 스킬 쿨타임]
            if (isSkillOn)
            {
                skillTime += Time.deltaTime;
                if (skillTime >= 5)
                {
                    isSkillOn = false;
                    skillTime = 0;
                    moveSpeed = 5f;
                }
            }
            //[플레이어 사망]
            if (currHp <= 0)
            {
                isDie = true;
                animator.SetTrigger("isDie");
                Destroy(this.gameObject, 3f);
            }
            //[씨앗 심기 가능]
            //Planting();
            DisplayStatusbar();
        }
    }

    //bool isGetSeed; //씨앗 획득 여부      (참 : 가능, 거짓 : 불가능)
    //bool isPlant; //씨앗 심기 불/가능 여부 ( 참 : 가능, 거짓 : 불가능 )
    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stage")
        {
            if (isJump == true)
            {
                isJump = false;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Stage")
        {
            if (isAblePlant == true) //씨앗획득
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    // 게임매니저 싱글턴을 활용해서 floorArray값 연산 
                    // string floorIdstr = collision.gameObject.GetComponent<floor>().floorId;
                    // print(floorIdstr);
                    // string[] Result = floorIdstr.Split(new char[] { ' ' });
                    // int a, b;
                    // a = System.Convert.ToInt32(Result[0]);
                    // b = System.Convert.ToInt32(Result[1]);
                    // GameManager.instance.floorArray[a, b] = "R";
                    // 포맷

                    isGetSeed = false;
                    PlantArea = Instantiate(PlantArea, collision.transform.position + new Vector3(0, 0.35f, 0), Quaternion.identity);
                    redFloor = Instantiate(redFloor, collision.transform.position + new Vector3(0, -0.49f, 0), Quaternion.identity);
                    plantCount++;
                    isAblePlant = false;//씨앗심기 불가능
                }
            }
        }
    }
    void DisplayStatusbar()
    {
        hpBar.fillAmount = (currHp / maxHp); // 체력게이지
        mpBar.fillAmount = (currMp / maxMp); // 마나게이지
    }
}



