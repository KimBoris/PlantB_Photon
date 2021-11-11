using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireCtrl : MonoBehaviour
{
        public GameObject bullet; //불렛
        public Transform firePos; //총알 발사 위치

    public float maxBullet = 20;//최대 총알
    public float remainingBullet = 20;//남은 총알

    public float reloadTime = 2f;
   public bool isReloading = false;

    public Image bulletsbar;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isReloading && Input.GetMouseButtonDown(0))
        {   //장전중 아닐때

            Instantiate(bullet, firePos.position, firePos.rotation);
            remainingBullet--;
            if (remainingBullet == 0)
            {
                StartCoroutine(Reloading());
            }
        }
        else if (!isReloading && Input.GetKeyDown(KeyCode.R) && remainingBullet != 20)
        {   //총알이 19발 이하일 때! (중간 장전)
            StartCoroutine(Reloading());
        }
        bulletReload();

    }
    IEnumerator Reloading() //재장전 코루틴함수
    {
        isReloading = true;
        yield return new WaitForSeconds(2f);

        isReloading = false;
        remainingBullet = 20;
    }

    void bulletReload()
    {
        bulletsbar.fillAmount = (remainingBullet / maxBullet);
    }

}
