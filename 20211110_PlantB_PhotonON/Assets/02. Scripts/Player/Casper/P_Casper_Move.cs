using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Casper_Move : MonoBehaviour
{
    Vector3 dir;

    float moveSpeed;
    float rotateSpeed;
    float jumpPower;//점프력
    bool isJump;//점프 여부

    PlayerInput PInput;
    P_Casper_Status casperStatus;

    Rigidbody rigid;
    Animator casperAnim;

    private void OnEnable()
    {
        moveSpeed = 7f;
        rotateSpeed = 100f;
        jumpPower = 5;
        isJump = false;

        rigid = GetComponent<Rigidbody>();
        PInput = GetComponent<PlayerInput>();
        casperAnim = GetComponent<Animator>();
        casperStatus = GetComponent<P_Casper_Status>();
    }


    void FixedUpdate()
    {
        Move();
        Rotate();
    }
    private void Update()
    {
        if (isJump == false)
        {
            Jump();
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
        casperAnim.SetFloat("moveSpeed", dir.magnitude);
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
        float turn = PInput.rotate * rotateSpeed * Time.deltaTime;
        rigid.rotation = rigid.rotation * Quaternion.Euler(0, turn, 0);
    }
}
