using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
   
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InputPlayerID(string Id)
    {
        Debug.Log(Id);
        GameDataManager.instance.gameData.UserId = Id;
        Debug.Log(GameDataManager.instance.gameData.UserId);
        GameDataManager.instance.SaveGameData();
    }
    public void InputPlayerPW(string PassWord)
    {
        GameDataManager.instance.gameData.UserPw = PassWord;
        Debug.Log(GameDataManager.instance.gameData.UserPw);
        GameDataManager.instance.SaveGameData();
    }


}
