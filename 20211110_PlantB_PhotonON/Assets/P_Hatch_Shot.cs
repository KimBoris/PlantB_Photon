using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class P_Hatch_Shot : MonoBehaviour
//�ݹ�, �߻�, UIüũ����
{

    //[�Է�]
    PlayerInput playerInput;

    //������Ʈ Ǯ��[�Ѿ�]
    ObjPooling objPooling;

    string[] rifleBullets; //������Ʈ Ǯ������ ������ ������ �ҷ�(string)//<<

    //[��]
    public enum State//���� ���� ����
    {
        Ready, //ź��, �ݹ� �غ��
        Empty, //źâ�� ��
        Reloading //��������
    }
    public State state { get; private set; }//���� �������

    public Transform firePos; //�Ѿ��� �߻�� ��ġ
    public ParticleSystem muzzleFlashEff; //�ѱ��� ȭ�� ȿ��
    GameObject rifleBullet; //�Ѿ�

    float capacity = 20; //źâ �ִ� �Ѿ� ��
    float remainBullet = 0;//���� �Ѿ� ��


    //[�� ���� UI]
    Image bulletFillImg;   //�Ѿ��� ���� ���� �̹���
    Text bulletLeftText;//�Ѿ��� ���� ����

    //[����]
    private AudioSource rifleAudio; //���� ���� �����
    AudioClip shotClip;//�� �߻� �Ҹ�

    //[�߻�]
    float lastFireTime; //���������� ���� �� ����
    float fireDelay = 0.25f;//�Ѿ� �߻� ����
    float reloadTime = 2.2f;//���� �ð�

    //��������� ���� ĳ���ϴ� ���
    bool isMouseClick => Input.GetMouseButton(0); //���콺 ����Ű Ŭ��
    bool isReloadButton => Input.GetKeyDown(KeyCode.R);//RŰ 

    //[����]
    PhotonView pv;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        playerInput = GetComponent<PlayerInput>();
        objPooling = GameObject.Find("ObjectPooling").GetComponent<ObjPooling>();
        rifleBullets = new string[] { "rifleBullet" }; //������Ʈ Ǯ������ ���� �̸�
        bulletFillImg = GameObject.Find("BulletFill(Image)").GetComponent<Image>();
        bulletLeftText = GameObject.Find("BulletLeftText(Image)").GetComponent<Text>();

        //remainBullet = capacity; //�Ѿ� ���� ä���־��ֱ�
        //state = State.Ready; //���� ���� ����
        //lastFireTime = 0; //���������� �� �߻���� 0
    }
    private void OnEnable()
    {
        remainBullet = capacity;    //������ ����� ���� �������ֱ� ���ؼ� OnEnable�� ����
        lastFireTime = 0; //���������� �� �߻���� 0
        state = State.Ready;

        //������ ������ �ɷ�ġ ���� �������ִ� �Լ� �־��ֱ�
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            RelateShotBtn();//��ư �Է��� ó���ϴ� �Լ� �ݹ�, ���ε�
        }
    }
    void Fire()
    {//�߻縦 '�õ�' = �ٷ� ��°��� �ƴ϶� ���콺 Ŭ���� �����Ѵ�
        if (state == State.Ready && Time.time > lastFireTime + fireDelay)
        {//���� ����, (������ �� ����+�߻� ������) Ȯ��
            lastFireTime = Time.time; //������ ���� �� ������ Time.time�� �־��־� �ʱ�ȭ

            //���� �߻�Ǵ� �Լ� �־��ֱ�
            Shot();
        }
    }
    [PunRPC]
    void Shot()
    {//������ �߻� ��Ű�� �Լ�

        makeBullet(); //�����Ǿ��ִ� �Ѿ� �ҷ�����

        muzzleFlashEff.Play();//�����÷��ø� �����Ų��.(�ѱ�ȭ��)
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
        {//���ε� ���̰ų�, �Ѿ��� źâ���� ���� �۵� X(20/20)
            return;
        }
        StartCoroutine(ReloadRoutine());//���� �����ϴ� �Լ�

      
        return;

    }

    IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        yield return new WaitForSeconds(reloadTime);
        remainBullet = capacity; //�Ѿ� ����

        state = State.Ready; //������¸� Ready�� �ٲ��ش�.
    }

    void makeBullet()
    {//������Ʈ Ǯ������ ������ �Ѿ��� ������ ����ϴ� �Լ�
        rifleBullet = objPooling.MakeObj(rifleBullets[0]);
        //ObjPooling.MakeObj�Լ��� rifleBullets[0]�Ű����� �־��ֱ�
        if (rifleBullet != null)
        {
            rifleBullet.transform.position = firePos.position;//�Ѿ��� ��ġ�� FirePos
            rifleBullet.transform.rotation = firePos.rotation;//�Ѿ��� ������ FirePos
        }
    }

    void GunState()
    {//���� �Ѿ˼��� ������ �߻�� UIüũ
        bulletFillImg.color = Color.yellow;//�Ѿ� ���� ��Ÿ���� UI = ����� Fill
        bulletFillImg.fillAmount = (remainBullet / capacity);
        bulletLeftText.text = string.Format("{0}", remainBullet);//remainBullet�� text�� ��Ÿ����
        if (remainBullet < 11)//�Ѿ��� 11���� ������
        {
            bulletFillImg.color = Color.red;
        }
    }

    void RelateShotBtn()//�Ѱ� ���õ� ��ư �Է½�
    {
        if (isMouseClick)
        {
            Fire();
        }
        else if (isReloadButton)
        {
            Reload();
        }
        //���� �Ѿ��� ��Ÿ���� UI üũ �Լ��־��ֱ�
        GunState();
    }
}
