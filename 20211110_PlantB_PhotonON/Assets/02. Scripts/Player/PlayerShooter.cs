using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerShooter : MonoBehaviour
{
    public Rifle rifle;
    private PlayerInput playerInput;
    private Animator playerAnim;
    //public Transform gunPivot; //총 배치의 기준점
    //public Transform leftHandMount;//총의 왼쪽 손잡이
    //public Transform rightHandMount;//총의 오른쪽 손잡이

    PhotonView pv;
    
    //성능향상을 위해서 캐싱
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
    //    //슈터가 활성화 될 때 총도 함께 활성화
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

    //아바타 IK설정
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
