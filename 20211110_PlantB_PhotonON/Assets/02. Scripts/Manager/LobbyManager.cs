using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviour
{
    int curCoin;        //�÷��̾� ����
    int curRuby;     //�÷��̾� ũ����Ż
    int PlayerLevel;    //�÷��̾� ����
    float PlayerExp;      //�÷��̾� ����ġ
    string PlayerId;//�÷��̾� ���̵� = �г���

    public Text curCoinText;
    public Text curRubyText;
    public Text curPlayerIdText;//���� ����


    private void Awake()
    {
        GameDataManager.instance.LoadGameData();

        PlayerId = GameDataManager.instance.gameData.UserId;//������ ���̵�
        curCoin = GameDataManager.instance.gameData.UserCoin;//������ ������ �ʱ�ȭ
        curRuby = GameDataManager.instance.gameData.UserRuby;//������ ��� �ʱ�ȭ
        PlayerLevel = GameDataManager.instance.gameData.UserLevel;//������ �����ʱ�ȭ
        PlayerExp = GameDataManager.instance.gameData.UserExp;//������ ����ġ �ʱ�ȭ
    }
    private void Start()
    {
        curPlayerIdText.text = string.Format("{0}", PlayerId);
        curCoinText.text = string.Format("{0}", curCoin);
        curRubyText.text = string.Format("{0}", curRuby);
    }
    private void Update()
    {
        RubyCoinCheat();//��� ���� ġƮ
        ResetAllCount();//��� ���� �ʱ�ȭ
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
