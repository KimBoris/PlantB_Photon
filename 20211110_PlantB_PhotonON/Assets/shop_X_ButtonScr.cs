using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class shop_X_ButtonScr : MonoBehaviour
{

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void shopXButton()
    {
        SceneManager.LoadScene("Lobby");
        GameDataManager.instance.SaveGameData();
    }
}
