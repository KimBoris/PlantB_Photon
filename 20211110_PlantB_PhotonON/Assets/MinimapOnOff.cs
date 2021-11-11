using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//�÷��� �������� �� �� �ֵ���
using UnityEngine.UI;
public class MinimapOnOff : MonoBehaviour
{
    public RawImage minimap;
    public Text minimapOnOffText;
    void Start()
    {
        minimap = gameObject.GetComponent<RawImage>();
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")//�÷��� �������� �۵�
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                minimap.enabled = !minimap.enabled;
                if (minimap.enabled == true)
                {
                    minimapOnOffText.color = Vector4.one;
                }
                else
                {
                    minimapOnOffText.color = new Vector4(1, 1, 1, 0.5f);
                }
            }
        }
    }

}
