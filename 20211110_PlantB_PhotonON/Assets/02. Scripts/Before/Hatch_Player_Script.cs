using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hatch_Player_Script : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    float moveX; //����
    float moveZ; //����
    float rotateY; //����ȸ��
    float rotateX; //����ȸ��

    float moveSpeed; //�̵� �ӵ�
    float rotSpeed;  //ȸ�� �ӵ�
    float jumpPower; //�����Ŀ�

    public float currHp; //���� ü��
    public float currMp; //���� ����
    float maxHp; // �ִ� ü��
    float maxMp; // �ִ� ����

    float skillTime;//��ų ���� �ð�

    float spawnAreaTime;//���������� �ӹ��� �ð�

    Vector3 Dir; //�÷��̾� �̵� ����

    Transform tr;
    Rigidbody rb;

    bool isMove;//�ִϸ��̼� �����
    bool isDie; //�÷��̾� ���
    bool isSkillOn; //��ų ��� ����
    bool isGetSeed; //���� ȹ�� ����      (�� : ����, ���� : �Ұ���)
    bool isAblePlant; //���� �ɱ� ��/���� ���� ( �� : ����, ���� : �Ұ��� )
    bool isGetFloor; //�� ���� ����        
    bool isJump; //���� ����

    public GameObject PlantArea; //���� ���� ����
    public GameObject eff_Seed;  //���� ȹ��� ��ƼŬ
    public Image hpBar;  //ü�¹�
    public Image mpBar;  //������

    public GameObject[] rifleBullet;  //������ �Ѿ�
    public GameObject sgBullet;     //���� �Ѿ�
    public GameObject canonBullet;  //��ǥ �Ѿ�
    public GameObject redFloor; //���� ������ �÷��̾� �������� �ٲ�� �ٴ�
    public float plantCount; //������ ������

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
            //[�÷��̾� �̵�]
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            rotateY = Input.GetAxis("Mouse X");
            rotateX = Input.GetAxis("Mouse Y");

            Dir = new Vector3(moveX, 0f, moveZ);
            Dir = Dir.normalized;

            tr.Translate(Dir * Time.deltaTime * moveSpeed, Space.Self); //�÷��̾� ����
            tr.Rotate(new Vector3(0, rotateY, 0) * Time.deltaTime * rotSpeed); //�÷��̾� ȸ��(����)

            //[�÷��̾� ����]
            if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Force);
                isJump = true;
            }

            //[�ִϸ��̼�]
            isMove = Dir.magnitude != 0;                   //�÷��̾� �̵����Ͱ� ( �ִϸ��̼� ������ ���ؼ� )
            animator.SetFloat("moveSpeed", Dir.magnitude); //�ƹ�Ÿ ����ũ (��ü - ��, ��ü - �޸���, ����)

            //Hp, Mp Bar

            //[�÷��̾� ����]
            if (currMp <= 100)
            {
                currMp += Time.deltaTime;
                if (currMp > 100)
                {
                    currMp = 100;
                }
            }
            //[�÷��̾� ��ų] - ���� : 5�ʰ� �̵��ӵ� 2�� ����
            if (currMp >= 100)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    moveSpeed = 10f;
                    Debug.Log("��ų���!!");
                    currMp -= 100;
                    isSkillOn = false;
                }
            }
            //[�÷��̾� ��ų ��Ÿ��]
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
            //[�÷��̾� ���]
            if (currHp <= 0)
            {
                isDie = true;
                animator.SetTrigger("isDie");
                Destroy(this.gameObject, 3f);
            }
            //[���� �ɱ� ����]
            //Planting();
            DisplayStatusbar();
        }
    }

    //bool isGetSeed; //���� ȹ�� ����      (�� : ����, ���� : �Ұ���)
    //bool isPlant; //���� �ɱ� ��/���� ���� ( �� : ����, ���� : �Ұ��� )
    


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
            if (isAblePlant == true) //����ȹ��
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    // ���ӸŴ��� �̱����� Ȱ���ؼ� floorArray�� ���� 
                    // string floorIdstr = collision.gameObject.GetComponent<floor>().floorId;
                    // print(floorIdstr);
                    // string[] Result = floorIdstr.Split(new char[] { ' ' });
                    // int a, b;
                    // a = System.Convert.ToInt32(Result[0]);
                    // b = System.Convert.ToInt32(Result[1]);
                    // GameManager.instance.floorArray[a, b] = "R";
                    // ����

                    isGetSeed = false;
                    PlantArea = Instantiate(PlantArea, collision.transform.position + new Vector3(0, 0.35f, 0), Quaternion.identity);
                    redFloor = Instantiate(redFloor, collision.transform.position + new Vector3(0, -0.49f, 0), Quaternion.identity);
                    plantCount++;
                    isAblePlant = false;//���ѽɱ� �Ұ���
                }
            }
        }
    }
    void DisplayStatusbar()
    {
        hpBar.fillAmount = (currHp / maxHp); // ü�°�����
        mpBar.fillAmount = (currMp / maxMp); // ����������
    }
}



