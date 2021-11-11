using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireCtrl : MonoBehaviour
{
        public GameObject bullet; //�ҷ�
        public Transform firePos; //�Ѿ� �߻� ��ġ

    public float maxBullet = 20;//�ִ� �Ѿ�
    public float remainingBullet = 20;//���� �Ѿ�

    public float reloadTime = 2f;
   public bool isReloading = false;

    public Image bulletsbar;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isReloading && Input.GetMouseButtonDown(0))
        {   //������ �ƴҶ�

            Instantiate(bullet, firePos.position, firePos.rotation);
            remainingBullet--;
            if (remainingBullet == 0)
            {
                StartCoroutine(Reloading());
            }
        }
        else if (!isReloading && Input.GetKeyDown(KeyCode.R) && remainingBullet != 20)
        {   //�Ѿ��� 19�� ������ ��! (�߰� ����)
            StartCoroutine(Reloading());
        }
        bulletReload();

    }
    IEnumerator Reloading() //������ �ڷ�ƾ�Լ�
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
