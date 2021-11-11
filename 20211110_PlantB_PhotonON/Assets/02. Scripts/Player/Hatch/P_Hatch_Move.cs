using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
public class P_Hatch_Move : MonoBehaviour
{
    PlayerInput PInput; //�÷��̾� �Է¹޴� ������Ʈ
    Transform transform;
    Animator hatchAnim; //��ġ �ִϸ��̼�
    Camera camera;

    //Plane plane;
    //Vector3 hitPoint;

    PhotonView pv;
    CinemachineVirtualCamera virtualCamera; //�ó׸ӽ� �����ī�޶�

    //���ŵ� ������
    Vector3 receivePos;
    Quaternion receiveRot;
    //���ŵ� ��ǥ�� �̵� �� ȸ���� �ӵ�
    float damping = 100f;



    Vector3 dir; //�÷��̾� �̵�����

    public float moveSpeed;
    float rotateSpeed;
    float jumpPower;//������
    bool isJump;//���� ����

    P_Hatch_Status hatchStatus; //��ġ�� ���� ��ũ��Ʈ

    Rigidbody rigid;    //������ٵ�


    private void OnEnable()
    {
        PInput = GetComponent<PlayerInput>();
        transform = GetComponent<Transform>();
        hatchAnim = GetComponent<Animator>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        if (pv.IsMine)//���� ���� �Ǵ�
        {
            //ī�޶� ������ �÷��̾��
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

        // plane = new Plane(transform.up, transform.position);

        moveSpeed = 5f;
        rotateSpeed = 100f;
        jumpPower = 5f;
        isJump = false;

        rigid = GetComponent<Rigidbody>();
        hatchStatus = GetComponent<P_Hatch_Status>();
    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            Move();
            Rotate();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * damping);
            transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);
        }
    }
    private void Update()
    {//fixedUPdate�� ����� �ѹ��� �ʱ� ������ update�� �з�
        if (pv.IsMine)
        {
            if (isJump == false)
            {
                Jump();
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * damping);
            transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stage")
        {
            isJump = false;
        }
    }

    void Move()
    {
        dir = new Vector3(PInput.moveH, 0, PInput.moveV);
        dir = dir.normalized;
        rigid.transform.Translate(dir * moveSpeed * Time.deltaTime);
        hatchAnim.SetFloat("moveSpeed", dir.magnitude);//�̵����� �����ؼ� �ִϸ��̼� ����
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    void Rotate()
    {
        //ȸ�� ��ġ
        float turn = PInput.rotate * rotateSpeed * Time.deltaTime;
        rigid.rotation = rigid.rotation * Quaternion.Euler(0, turn, 0);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//����(����) ĳ������ ��� �ڽ��� �����͸� �ٸ� �������� �۽�
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else//����
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }

    }

}
