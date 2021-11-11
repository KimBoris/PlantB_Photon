using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviour
{
    int curCoin;        //플레이어 코인
    int curRuby;     //플레이어 크리스탈
    int PlayerLevel;    //플레이어 레벨
    float PlayerExp;      //플레이어 경험치
    string PlayerId;//플레이어 아이디 = 닉네임

    public Text curCoinText;
    public Text curRubyText;
    public Text curPlayerIdText;//현재 레벨


    private void Awake()
    {
        GameDataManager.instance.LoadGameData();

        PlayerId = GameDataManager.instance.gameData.UserId;//유저의 아이디
        curCoin = GameDataManager.instance.gameData.UserCoin;//현재의 코인을 초기화
        curRuby = GameDataManager.instance.gameData.UserRuby;//현재의 루비를 초기화
        PlayerLevel = GameDataManager.instance.gameData.UserLevel;//현재의 레벨초기화
        PlayerExp = GameDataManager.instance.gameData.UserExp;//현재의 경험치 초기화
    }
    private void Start()
    {
        curPlayerIdText.text = string.Format("{0}", PlayerId);
        curCoinText.text = string.Format("{0}", curCoin);
        curRubyText.text = string.Format("{0}", curRuby);
    }
    private void Update()
    {
        RubyCoinCheat();//루비 코인 치트
        ResetAllCount();//루비 코인 초기화
    }
    public void ShopButton()
    {
        GameDataManager.instance.SaveGameData();
        SceneManager.LoadScene("Shop");
    }

    public void PlayButtonDown()
    {
        //StartCoroutine(LoadScene());
        SceneManager.LoadScene("PlayScene");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("PlayScene");
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            yield return null;
        }
    }
    void RubyCoinCheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameDataManager.instance.gameData.UserCoin += 100;
            GameDataManager.instance.gameData.UserRuby += 100;
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Lobby");
        }
    }
    void ResetAllCount()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GameDataManager.instance.gameData.UserCoin = 0;
            GameDataManager.instance.gameData.UserRuby = 0;
            GameDataManager.instance.SaveGameData();
            SceneManager.LoadScene("Lobby");
        }
    }
}
