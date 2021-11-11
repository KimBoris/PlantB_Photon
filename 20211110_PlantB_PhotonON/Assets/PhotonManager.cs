using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class PhotonManager : MonoBehaviourPunCallbacks
{

    readonly string version = "1.0";
    string userId = "BongKi";

    private void Awake()
    {
        //마스터 클라이언트의 씬 자동 동기화 옵션 설정
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;//버전
        PhotonNetwork.NickName = userId;//닉네임
                                        //포톤 서버와의 데이터 전송률 확인
        print(PhotonNetwork.SendRate);
        //포톤 서버 접속

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("접속을 하였습니다. 마스터에게");
        print($"PhotonNetwork.InLobby = " + PhotonNetwork.InLobby);
        //랜덤한 방을 찾아 접속
        PhotonNetwork.JoinRandomRoom();//로비에 접속
    }
    //방 입장 실패시 콜백함수 호출
    public override void OnJoinedLobby()
    {
        print($"PhotonNetwork.InLobby = " + PhotonNetwork.InLobby);
        //랜덤한 방을 찾아 접속 시도
        PhotonNetwork.JoinRandomRoom();//로비에 접속
    }

    //방입장에 실패했을 경우 호출되는 콜백함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("방 접속 실패" + returnCode + ":" + message);
        //새로 생성할 방 정보 설정
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 4; //최대 접속자 수
        roomOption.IsOpen = true;//공개 비공개 유무
        roomOption.IsVisible = true;//로비에서 룸 목록을 노출할 것인지

        PhotonNetwork.CreateRoom("방이름", roomOption);
    }

    //방 생성이 완료되면 호출되는 콜백함수
    public override void OnCreatedRoom()
    {
        print("방 생성 완료!");
        print("내 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
    }

    //방 입장한 후에 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        print("방 접속 유무 = " + PhotonNetwork.InRoom);
        print("접속 유저 수 = " + PhotonNetwork.CurrentRoom.PlayerCount);

        //방에 접속한 유저의 정보는 PhotonNetwork.CurrentRoom.Players에 있다.
        foreach (var Player in PhotonNetwork.CurrentRoom.Players)//접속시 플레이어들 닉네임 확인
        {
            print("접속한 유저의 닉네임 =" + Player.Value.NickName);
        }
        Transform[] SpawnPoints = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, SpawnPoints.Length);

        PhotonNetwork.Instantiate("Hatch", SpawnPoints[idx].position, SpawnPoints[idx].rotation, 0);
    }


    public void LeaveRoom()
    {
        Debug.Log("방을 떠나자");
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("방을 떠났다");
        SceneManager.LoadScene("Lobby");
        base.OnLeftRoom();
    }
    //public void OnLeaveLobby()
    //{
    //    Debug.Log("로비를 떠나자");
    //    로비 퇴장함수
    //    PhotonNetwork.LeaveLobby();
    //}
    //public override void OnLeftLobby()
    //{   //로비퇴장완료함수
    //    Debug.Log("로비를 떠났다");
    //    base.OnLeftLobby();
    //}
    public void OnDisconnect()//포톤 종료 함수
    {
        Debug.Log("종료");

        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }
}
