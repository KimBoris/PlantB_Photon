using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static GameDataManager _instance;
    public static GameDataManager instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "GameDataManager";
                _instance = _container.AddComponent(typeof(GameDataManager)) as GameDataManager;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = "PlantB_GameData.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                //불러오는함수
                LoadGameData();

            }
            return _gameData;
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
    }
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(fromJsonData);
        }
        else
        {
            _gameData = new GameData();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene("Start");
        }
    }
}



