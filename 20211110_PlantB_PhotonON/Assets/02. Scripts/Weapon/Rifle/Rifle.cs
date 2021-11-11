using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Rifle : MonoBehaviour
{

    public enum State
    {
        Ready, //ź���� �غ��
        Empty, //źâ�� ��
        Reloading //��������
    }

    public Transform firePos;//ź���� �߻�� ��ġ
    public GameObject rifleBullet;
    public ParticleSystem muzzleFlashEff;//�ѱ� ȭ�� ȿ��

    PhotonView pv;

    Image gunImage;
    Text gunStateText;
    public State state { get; private set; } //���� ����
    PlayerInput PInput;

    ObjPooling objPooling;

    public string[] rifleBullets;//������Ʈ Ǯ������ ������ ������ �ҷ�(string)

    private AudioSource rifleAudio; //�� ���� �����
    public AudioClip shotClip; //�߻� �Ҹ�

    public float capacity = 20; //źâ �ִ� �Ѿ˼�
    public float remainBullet = 0; //���� �Ѿ� ��


    public float lastFireTime; //������ ���� �� ����
    public float fireDelay = 0.25f;//�Ѿ� �߻� ����
    public float reloadTime = 2.2f; //������ �ð�
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
        remainBullet = capacity; //�Ѿ� ���� ä���
        state = State.Ready;
        lastFireTime = 0;
    }


    public void Fire()
    {//�߻� �õ� �Լ�
     //State.Ready && �Ѿ� �߻簣���� �´ٸ�
        if (state == State.Ready && Time.time >= lastFireTime + fireDelay)
        {
            lastFireTime = Time.time;
            //���� �߻�
            Shot();
        }
    }

    [PunRPC]
    public void Shot()//���� �߻� �Լ�
    {
        //�Ѿ� ����
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
        {//������ ��, ���� �Ѿ��� źâ�ִ���� ���ų� Ŭ���� �۵� X
            return;
        }
        StartCoroutine(ReloadRoutine());
        return;
    }
    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;

        yield return new WaitForSeconds(reloadTime);
        remainBullet = capacity;//�Ѿ� ä���

        state = State.Ready;//���� ���� ����
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
    void GunState() //���� �Ѿ� ����
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
