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
        //������ Ŭ���̾�Ʈ�� �� �ڵ� ����ȭ �ɼ� ����
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = version;//����
        PhotonNetwork.NickName = userId;//�г���
                                        //���� �������� ������ ���۷� Ȯ��
        print(PhotonNetwork.SendRate);
        //���� ���� ����

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("������ �Ͽ����ϴ�. �����Ϳ���");
        print($"PhotonNetwork.InLobby = " + PhotonNetwork.InLobby);
        //������ ���� ã�� ����
        PhotonNetwork.JoinRandomRoom();//�κ� ����
    }
    //�� ���� ���н� �ݹ��Լ� ȣ��
    public override void OnJoinedLobby()
    {
        print($"PhotonNetwork.InLobby = " + PhotonNetwork.InLobby);
        //������ ���� ã�� ���� �õ�
        PhotonNetwork.JoinRandomRoom();//�κ� ����
    }

    //�����忡 �������� ��� ȣ��Ǵ� �ݹ��Լ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("�� ���� ����" + returnCode + ":" + message);
        //���� ������ �� ���� ����
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 4; //�ִ� ������ ��
        roomOption.IsOpen = true;//���� ����� ����
        roomOption.IsVisible = true;//�κ񿡼� �� ����� ������ ������

        PhotonNetwork.CreateRoom("���̸�", roomOption);
    }

    //�� ������ �Ϸ�Ǹ� ȣ��Ǵ� �ݹ��Լ�
    public override void OnCreatedRoom()
    {
        print("�� ���� �Ϸ�!");
        print("�� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
    }

    //�� ������ �Ŀ� ȣ��Ǵ� �ݹ��Լ�
    public override void OnJoinedRoom()
    {
        print("�� ���� ���� = " + PhotonNetwork.InRoom);
        print("���� ���� �� = " + PhotonNetwork.CurrentRoom.PlayerCount);

        //�濡 ������ ������ ������ PhotonNetwork.CurrentRoom.Players�� �ִ�.
        foreach (var Player in PhotonNetwork.CurrentRoom.Players)//���ӽ� �÷��̾�� �г��� Ȯ��
        {
            print("������ ������ �г��� =" + Player.Value.NickName);
        }
        Transform[] SpawnPoints = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, SpawnPoints.Length);

        PhotonNetwork.Instantiate("Hatch", SpawnPoints[idx].position, SpawnPoints[idx].rotation, 0);
    }


    public void LeaveRoom()
    {
        Debug.Log("���� ������");
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("���� ������");
        SceneManager.LoadScene("Lobby");
        base.OnLeftRoom();
    }
    //public void OnLeaveLobby()
    //{
    //    Debug.Log("�κ� ������");
    //    �κ� �����Լ�
    //    PhotonNetwork.LeaveLobby();
    //}
    //public override void OnLeftLobby()
    //{   //�κ�����Ϸ��Լ�
    //    Debug.Log("�κ� ������");
    //    base.OnLeftLobby();
    //}
    public void OnDisconnect()//���� ���� �Լ�
    {
        Debug.Log("����");

        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }
}
