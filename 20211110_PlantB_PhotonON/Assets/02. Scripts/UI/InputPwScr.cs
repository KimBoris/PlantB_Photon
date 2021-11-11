using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPwScr : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

   public void InputId(string Id)
    {
        GameDataManager.instance.gameData.UserId = Id;
        Debug.Log(Id);
        GameDataManager.instance.SaveGameData();
    }
   
}
