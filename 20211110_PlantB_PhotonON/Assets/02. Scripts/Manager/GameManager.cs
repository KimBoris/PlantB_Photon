using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviour
{
    //public  PhotonHandler photonHandler;
    PhotonManager photonManager;
    public static GameManager instance = null;
    public GameObject Seed;
    public GameObject bulletprefab;

    PhotonView pv;


    //[게임오버]
    public GameObject GameOverUiSet; //게임오버 셋

    public string[,] floorArray = new string[5, 5];
    public bool isGameover;//게임오버
    public float playTime; //플레이 제한시간 100초
    public float gameoverDelayTime; //게임 오버시에 로비로 넘어가기전 걸리는 시간

    float delay; //씨앗 생성 딜레이
    float produce; //씨앗 생성
    float SeedX; //씨앗 생성 X좌표
    float SeedZ; //씨앗 생성 Z좌표
    float FullSeed; //씨앗의 최대 생성갯수


    public Text PlayTimeText;//UI사용을 위해서만듬.

    public int curCoin;
    public int curRuby;
    public Text MoneyUI;
    public Text rubyUI;
    //public int maxpool = 10;
    ////  public List<GameObject> bulletpool = new List<GameObject>();

    bool isBingo = false; //빙고 여부
                          //게임 오버
                          //bool isplanted = false; //점령 여부
    Hatch_Player_Script hs;

    public int player1Score; // 플레이어 1 점수
    public int player2Score; // 플레이어 2 점수 
    public int player3Score; // 플레이어 3 점수 
    public int player4Score; // 플레이어 4 점수 

    int fullOccupation; //UI 사용을 위해서 Left, Right Score 13로 기준

    public Image LeftScore;
    public Image RightScore;



    public enum GameResult
    {
        Ready,   //게임시작 준비
        playing, //게임중
        player1,   //플레이어 승리
        player2,   //플레이어 승리
        player3,   //플레이어 승리
        player4,   //플레이어 승리
        draw       //무승부
    }
    public GameResult gameResult;


    private void Awake()
    {
        photonManager = GameObject.Find("PhotonManager").GetComponent<PhotonManager>();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        PlayTimeText = GameObject.Find("Time(Text)").GetComponent<Text>();
        Time.timeScale = 1;
        player1Score = 0;//플레이어 1 스코어
        player2Score = 0;//플레이어 2 스코어
        playTime = 100; //플레이 시간
        gameResult = GameResult.Ready; //게임 상태

        fullOccupation = 13; //최대 점령 수(UI사용으로 인해)
        FullSeed = 0;

        //isGameover = false; //게임오버 다시 false로
        // CreatePooling();
        //  DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //GameResult.Ready 상태에서 플레이어가 최소 2명이 될때!
        //GameResult를 바꿔준다.

        gameResult = GameResult.playing;
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            GamePlayTime();
            seedProduce();//씨앗생성
            ScoreFillAmount();
        }
        if (isGameover == true)
        {
            gameoverDelayTime += Time.deltaTime;
            GameOver();
        }
    }

    //[게임 진행시 흘러가는 시간]
    void GamePlayTime()
    {
        if (playTime <= 0)
        {
            playTime = 0;
            isGameover = true;

            //게임의 상태를 변경해주는 코드(승자체크)
            WinnerCheck();
            //게임 오버 UI 띄워주기 및 상태 재변경
            GameOver();
        }
        playTime -= Time.deltaTime;
        PlayTimeText.text = string.Format("{0}", (int)playTime);
    }

    public void GameOver()
    {
        //GameOverDelayTime();

        GameOverUiSet.SetActive(true);//게임오버 UI활성화

        if (gameoverDelayTime > 5)
        {
            gameoverDelayTime = 0;
            GameOverUiSet.SetActive(false);
            gameResult = GameResult.Ready;//준비상태로
            isGameover = false;
            playTime = 100;
            SceneManager.LoadScene("Lobby");
            //if (SceneManager.GetActiveScene().name == "PlayScene")
            //{
            ////Destroy(photonHandler);
            //photonManager.LeaveRoom();
            //photonManager.OnLeftRoom();
            ////photonManager.OnLeaveLobby();
            //photonManager.OnLeftLobby();
            photonManager.OnDisconnect();
            //}
        }

    }


    //[타임 오버 시] 많이 점령한 팀이 승리
    public void WinnerCheck()
    {
        if (player1Score > player2Score)
        {
            gameResult = GameResult.player1;

            //플레이어 1승리 띄우기
        }
        else if (player1Score < player2Score)
        {
            gameResult = GameResult.player2;
            //플레이어 2승리 띄우기
        }
        else if (player1Score == player2Score)
        {//무승부
            gameResult = GameResult.draw;
        }
        //isGameover = true;
        //Time.timeScale = 0.1f;

    }

    //[씨앗 생성 코드]
    void seedProduce()
    {
        SeedX = Random.Range(-10f, 36f);
        SeedZ = Random.Range(-30f, 12f);

        produce += Time.deltaTime;
        delay = 2f;

        if (produce >= delay)
        {
            Instantiate(Seed, new Vector3(SeedX, 3, SeedZ), Quaternion.Euler(0, 0, 90));
            FullSeed++;
            produce = 0;
        }
        else if (FullSeed > 25)
        {
            produce = 0;
        }
    }


    void ScoreFillAmount()
    {//점수 게이지
        LeftScore.fillAmount = ((float)player1Score / fullOccupation);
        RightScore.fillAmount = ((float)player2Score / fullOccupation);

    }

    //void Rubys()
    //{
    //    rubyUI.text = string.Format("{0:N0}", Ruby);
    //}
    //void Moneys()
    //{
    //    MoneyUI.text = string.Format("{0:N0}", Money);
    //}
}