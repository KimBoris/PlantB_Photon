using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerShooter : MonoBehaviour
{
    public Rifle rifle;
    private PlayerInput playerInput;
    private Animator playerAnim;
    //public Transform gunPivot; //�� ��ġ�� ������
    //public Transform leftHandMount;//���� ���� ������
    //public Transform rightHandMount;//���� ������ ������

    PhotonView pv;
    
    //��������� ���ؼ� ĳ��
    bool isMouseClick => Input.GetMouseButtonDown(0);
    
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    void Start()
    {

        rifle = gameObject.transform.Find("w_rifle").GetComponent<Rifle>();
        playerInput = GetComponent<PlayerInput>();
        playerAnim = GetComponent<Animator>();
    }


    //private void OnEnable()
    //{
    //    //���Ͱ� Ȱ��ȭ �� �� �ѵ� �Բ� Ȱ��ȭ
    //    rifle.gameObject.SetActive(true);

    //}
    //private void OnDisable()
    //{
    //    rifle.gameObject.SetActive(false);
    //}

    private void Update()
    {
        if (pv.IsMine)
        {
            if (isMouseClick)
            {
                rifle.Fire();

            }
            else if (playerInput.reload)
            {
                rifle.Reload();
            }
        }
    }

    //�ƹ�Ÿ IK����
    //private void OnAnimatorIK(int layerIndex)
    //{
    //    gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

    //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
    //    playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

    //    playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
    //    playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    //}


}
