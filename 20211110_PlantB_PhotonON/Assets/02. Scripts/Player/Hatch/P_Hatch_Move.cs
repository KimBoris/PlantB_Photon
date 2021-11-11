using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
public class P_Hatch_Move : MonoBehaviour
{
    PlayerInput PInput; //플레이어 입력받는 컴포넌트
    Transform transform;
    Animator hatchAnim; //해치 애니메이션
    Camera camera;

    //Plane plane;
    //Vector3 hitPoint;

    PhotonView pv;
    CinemachineVirtualCamera virtualCamera; //시네머신 버츄어카메라

    //수신된 데이터
    Vector3 receivePos;
    Quaternion receiveRot;
    //수신된 좌표로 이동 및 회전할 속도
    float damping = 100f;



    Vector3 dir; //플레이어 이동방향

    public float moveSpeed;
    float rotateSpeed;
    float jumpPower;//점프력
    bool isJump;//점프 여부

    P_Hatch_Status hatchStatus; //해치의 상태 스크립트

    Rigidbody rigid;    //리지드바디


    private void OnEnable()
    {
        PInput = GetComponent<PlayerInput>();
        transform = GetComponent<Transform>();
        hatchAnim = GetComponent<Animator>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        if (pv.IsMine)//나와 남을 판단
        {
            //카메라 초점을 플레이어에게
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
    {//fixedUPdate로 실행시 한박자 늦기 때문에 update로 분류
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
        hatchAnim.SetFloat("moveSpeed", dir.magnitude);//이동값을 감지해서 애니메이션 실행
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
        //회전 수치
        float turn = PInput.rotate * rotateSpeed * Time.deltaTime;
        rigid.rotation = rigid.rotation * Quaternion.Euler(0, turn, 0);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)//로컬(본인) 캐릭터인 경우 자신의 데이터를 다른 유저에게 송신
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else//수신
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }

    }

}
