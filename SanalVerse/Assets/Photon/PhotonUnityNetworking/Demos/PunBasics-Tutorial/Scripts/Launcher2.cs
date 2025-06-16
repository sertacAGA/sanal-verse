using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Launcher2 : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField roomNameInput;
    [SerializeField] private GameObject roomListPanel;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private byte maxPlayersPerRoom = 20;

    private bool isConnecting = false;
    private string gameVersion = "1";

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public void Connect()
    {
        isConnecting = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Attempting to connect to Photon...");
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            string roomName = roomNameInput.text;
            if (!string.IsNullOrEmpty(roomName))
            {
                RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayersPerRoom };
                PhotonNetwork.CreateRoom(roomName, roomOptions);
                Debug.Log("Attempting to create room: " + roomName);
            }
            else
            {
                Debug.LogError("Room name is empty.");
            }
        }
        else
        {
            Debug.LogError("Cannot create room. Not connected to Photon or not in a lobby.");
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master.");
        if (isConnecting)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Joining Lobby...");
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby.");
        isConnecting = false;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected: {0}", cause);
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room.");

        // Load "Karakter" level if joined room successfully
        PhotonNetwork.LoadLevel("Karakter");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join Random Failed. Creating a new room.");
        string roomName = "Room" + Random.Range(1000, 9999);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayersPerRoom };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("Create Room Failed: {0} - {1}", returnCode, message);
    }

    public void ShowRoomList()
    {
        if (PhotonNetwork.InLobby)
        {
            // Implement logic to get and display the room list
        }
        else
        {
            Debug.LogError("Not in lobby. Cannot show room list.");
        }
    }

    // Method to be called by UI Button to ensure the connection process is complete before creating a room
    public void OnCreateRoomButtonClicked()
    {
        Debug.Log("Create Room Button Clicked.");
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            CreateRoom();
        }
        else
        {
            Debug.LogError("Cannot create room. Not connected to Photon or not in a lobby.");
        }
    }
}
