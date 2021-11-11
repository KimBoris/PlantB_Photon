using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//플레이 씬에서만 할 수 있도록
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
        if (SceneManager.GetActiveScene().name == "PlayScene")//플레이 씬에서만 작동
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
