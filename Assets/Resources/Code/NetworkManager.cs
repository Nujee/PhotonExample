using Photon.Pun;
using UnityEngine;

public sealed class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby! Create or enter the room...");
        PhotonNetwork.JoinOrCreateRoom("BattleRoom", new Photon.Realtime.RoomOptions { MaxPlayers = 4 }, default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully entered the room");
        PhotonNetwork.Instantiate("PlayerCube", new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)), Quaternion.identity);
    }
}