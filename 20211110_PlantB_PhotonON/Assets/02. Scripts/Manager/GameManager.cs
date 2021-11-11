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


    //[���ӿ���]
    public GameObject GameOverUiSet; //���ӿ��� ��

    public string[,] floorArray = new string[5, 5];
    public bool isGameover;//���ӿ���
    public float playTime; //�÷��� ���ѽð� 100��
    public float gameoverDelayTime; //���� �����ÿ� �κ�� �Ѿ���� �ɸ��� �ð�

    float delay; //���� ���� ������
    float produce; //���� ����
    float SeedX; //���� ���� X��ǥ
    float SeedZ; //���� ���� Z��ǥ
    float FullSeed; //������ �ִ� ��������


    public Text PlayTimeText;//UI����� ���ؼ�����.

    public int curCoin;
    public int curRuby;
    public Text MoneyUI;
    public Text rubyUI;
    //public int maxpool = 10;
    ////  public List<GameObject> bulletpool = new List<GameObject>();

    bool isBingo = false; //���� ����
                          //���� ����
                          //bool isplanted = false; //���� ����
    Hatch_Player_Script hs;

    public int player1Score; // �÷��̾� 1 ����
    public int player2Score; // �÷��̾� 2 ���� 
    public int player3Score; // �÷��̾� 3 ���� 
    public int player4Score; // �÷��̾� 4 ���� 

    int fullOccupation; //UI ����� ���ؼ� Left, Right Score 13�� ����

    public Image LeftScore;
    public Image RightScore;



    public enum GameResult
    {
        Ready,   //���ӽ��� �غ�
        playing, //������
        player1,   //�÷��̾� �¸�
        player2,   //�÷��̾� �¸�
        player3,   //�÷��̾� �¸�
        player4,   //�÷��̾� �¸�
        draw       //���º�
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
        player1Score = 0;//�÷��̾� 1 ���ھ�
        player2Score = 0;//�÷��̾� 2 ���ھ�
        playTime = 100; //�÷��� �ð�
        gameResult = GameResult.Ready; //���� ����

        fullOccupation = 13; //�ִ� ���� ��(UI������� ����)
        FullSeed = 0;

        //isGameover = false; //���ӿ��� �ٽ� false��
        // CreatePooling();
        //  DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //GameResult.Ready ���¿��� �÷��̾ �ּ� 2���� �ɶ�!
        //GameResult�� �ٲ��ش�.

        gameResult = GameResult.playing;
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            GamePlayTime();
            seedProduce();//���ѻ���
            ScoreFillAmount();
        }
        if (isGameover == true)
        {
            gameoverDelayTime += Time.deltaTime;
            GameOver();
        }
    }

    //[���� ����� �귯���� �ð�]
    void GamePlayTime()
    {
        if (playTime <= 0)
        {
            playTime = 0;
            isGameover = true;

            //������ ���¸� �������ִ� �ڵ�(����üũ)
            WinnerCheck();
            //���� ���� UI ����ֱ� �� ���� �纯��
            GameOver();
        }
        playTime -= Time.deltaTime;
        PlayTimeText.text = string.Format("{0}", (int)playTime);
    }

    public void GameOver()
    {
        //GameOverDelayTime();

        GameOverUiSet.SetActive(true);//���ӿ��� UIȰ��ȭ

        if (gameoverDelayTime > 5)
        {
            gameoverDelayTime = 0;
            GameOverUiSet.SetActive(false);
            gameResult = GameResult.Ready;//�غ���·�
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


    //[Ÿ�� ���� ��] ���� ������ ���� �¸�
    public void WinnerCheck()
    {
        if (player1Score > player2Score)
        {
            gameResult = GameResult.player1;

            //�÷��̾� 1�¸� ����
        }
        else if (player1Score < player2Score)
        {
            gameResult = GameResult.player2;
            //�÷��̾� 2�¸� ����
        }
        else if (player1Score == player2Score)
        {//���º�
            gameResult = GameResult.draw;
        }
        //isGameover = true;
        //Time.timeScale = 0.1f;

    }

    //[���� ���� �ڵ�]
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
    {//���� ������
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