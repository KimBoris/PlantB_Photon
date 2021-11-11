using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCoinImage : MonoBehaviour
{
    float moveSpeed;
    float rotSpeed;
   public float moveX;
    float rotX;
    bool isMax;
    RectTransform coinPos;
    void Start()
    {
        moveSpeed = 200;
        rotSpeed = 100;
        coinPos = GetComponent<RectTransform>();
        Debug.Log(coinPos);
        moveX = 0;
    }

    void Update()
    {

        Movement();

    }
    void Movement()
    {
        if (gameObject != null)
        {
            moveX += Time.deltaTime * moveSpeed;
            rotX += Time.deltaTime * rotSpeed;
            coinPos.position = new Vector3(moveX, 315, 0);
            coinPos.rotation = Quaternion.Euler(0, rotX,0);
            if (moveX >= 1788||moveX <=0)
            {
                moveSpeed *= (-1);
            }
        }
        

      
    }


}
