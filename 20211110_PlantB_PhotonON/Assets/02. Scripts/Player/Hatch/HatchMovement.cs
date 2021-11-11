using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Realtime;
using Photon.Pun;

public class HatchMovement : MonoBehaviour
{
    CharacterController controller;
    Animator hatchAnimator; //플레이어 애니메이션 
    Transform transform;
    Camera camera;

    Plane plane; //오브젝트를 코드로 작성
    Ray ray;
    Vector3 hitPoint;

    public float moveSpeed = 5f; //이동속도
    float rotateSpeed = 100f; //회전속도
    float jumpPower = 5f;
    private PlayerInput playerInput;  //플레이어 입력받는 컴포넌트
    private Rigidbody hatchRigidbody; //해치 캐릭터 리지드 바디 
    public bool isJump; //점프 유무

    PhotonView pv; //포톤 뷰 컴포넌트 캐시 처리를 위한 변수
    CinemachineVirtualCamera virtualCamera;//시네머신 버츄어카메라

    //수신된 데이터
    Vector3 receivePos;
    Quaternion receiveRot;
    //수신된 좌표로 이동 및 회전할 속도
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

    void FixedUpdate() // 물리갱신 주기마다 실행, why? > Update보다 오차가 날 확률이 적기 때문에
    {
        if (pv.IsMine)
        {
            Move();
            Rotate();
        }
        else//타 플레이어의 경우 수신된 데이터로 조작해줘야한다
        {
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * damping);
            transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);
        }


    }
    private void Update()
    {
        Jump2(); //fixedUpdate에서는 한박자 느리게구현되어서 update로 이동
    }

    private void Move()
    {
        //캐릭터 이동 제어
        Vector3 dir = new Vector3(playerInput.moveH, 0, playerInput.moveV);
        hatchRigidbody.transform.Translate(dir * moveSpeed * Time.deltaTime);
        hatchAnimator.SetFloat("moveSpeed", dir.magnitude);
    }
    private void Rotate()
    {
        //상대적으로 회전할수치 계산
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        hatchRigidbody.rotation = hatchRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hatchRigidbody.velocity.y == 0)
        {
            hatchRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }//점프키가 안먹는 경우가 있어 보류
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
        if (stream.IsWriting)//로컬(본인) 캐릭터인경우 자신의 데이터를 다른 유저에게 송신
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
