using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCam : MonoBehaviour
{
    public Transform target;   //���� ���
    public float moveDamping = 1f;   //�̵��ӵ� ���
    public float distance = 3.5f;       //���� ������ �Ÿ�
    public float height = 2.4f;       //����
    public float targetOffset =1.8f;   //���� ��ǥ�� ������
    public float sideDis = 0f;
    Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height) - (target.right * sideDis); //�Ÿ� ����

        tr.position = Vector3.Slerp(tr.position, camPos, moveDamping);
        tr.LookAt(target.position + (target.up * targetOffset)); //ī�޶� Ÿ�ٹ���
    }

    private void OnDrawGizmos()
    {
        //[����׸� ���ؼ�]
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.1f);
        //ī�޶�,�÷��̾� ������ ��
        //Gizmos.DrawLine(target.position + (target.up * targetOffset), tr.position);

    }
}
