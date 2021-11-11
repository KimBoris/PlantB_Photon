using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Realtime;
using Photon.Pun;

public class HatchMovement : MonoBehaviour
{
    CharacterController controller;
    Animator hatchAnimator; //�÷��̾� �ִϸ��̼� 
    Transform transform;
    Camera camera;

    Plane plane; //������Ʈ�� �ڵ�� �ۼ�
    Ray ray;
    Vector3 hitPoint;

    public float moveSpeed = 5f; //�̵��ӵ�
    float rotateSpeed = 100f; //ȸ���ӵ�
    float jumpPower = 5f;
    private PlayerInput playerInput;  //�÷��̾� �Է¹޴� ������Ʈ
    private Rigidbody hatchRigidbody; //��ġ ĳ���� ������ �ٵ� 
    public bool isJump; //���� ����

    PhotonView pv; //���� �� ������Ʈ ĳ�� ó���� ���� ����
    CinemachineVirtualCamera virtualCamera;//�ó׸ӽ� �����ī�޶�

    //���ŵ� ������
    Vector3 receivePos;
    Quaternion receiveRot;
    //���ŵ� ��ǥ�� �̵� �� ȸ���� �ӵ�
    public float damping = 10f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        hatchAnimator = GetComponent<Animator>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();

        playerInput = GetComponent<PlayerInput>();
        hatchRigidbody = GetComponent<Rigidbody>();

        if (pv.IsMine)
        {
            virtualCamera.Follow = this.transform;
            virtualCamera.LookAt = this.transform;
        }
        plane = new Plane(transform.up, transform.position);
    }

    void FixedUpdate() // �������� �ֱ⸶�� ����, why? > Update���� ������ �� Ȯ���� ���� ������
    {
        if (pv.IsMine)
        {
            Move();
            Rotate();
        }
        else//Ÿ �÷��̾��� ��� ���ŵ� �����ͷ� ����������Ѵ�
        {
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * damping);
            transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);
        }


    }
    private void Update()
    {
        Jump2(); //fixedUpdate������ �ѹ��� �����Ա����Ǿ update�� �̵�
    }

    private void Move()
    {
        //ĳ���� �̵� ����
        Vector3 dir = new Vector3(playerInput.moveH, 0, playerInput.moveV);
        hatchRigidbody.transform.Translate(dir * moveSpeed * Time.deltaTime);
        hatchAnimator.SetFloat("moveSpeed", dir.magnitude);
    }
    private void Rotate()
    {
        //��������� ȸ���Ҽ�ġ ���
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        hatchRigidbody.rotation = hatchRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hatchRigidbody.velocity.y == 0)
        {
            hatchRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }//����Ű�� �ȸԴ� ��찡 �־� ����
    private void Jump2()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump == false)
            {
                hatchRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJump = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Stage")
        {
            isJump = false;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//����(����) ĳ�����ΰ�� �ڽ��� �����͸� �ٸ� �������� �۽�
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }
    }

}
