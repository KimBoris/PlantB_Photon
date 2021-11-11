using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCam : MonoBehaviour
{
    public Transform target;   //추적 대상
    public float moveDamping = 1f;   //이동속도 계수
    public float distance = 3.5f;       //추적 대상과의 거리
    public float height = 2.4f;       //높이
    public float targetOffset =1.8f;   //추적 좌표의 오프셋
    public float sideDis = 0f;
    Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height) - (target.right * sideDis); //거리 높이

        tr.position = Vector3.Slerp(tr.position, camPos, moveDamping);
        tr.LookAt(target.position + (target.up * targetOffset)); //카메라 타겟방향
    }

    private void OnDrawGizmos()
    {
        //[디버그를 위해서]
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.1f);
        //카메라,플레이어 사이의 줄
        //Gizmos.DrawLine(target.position + (target.up * targetOffset), tr.position);

    }
}
