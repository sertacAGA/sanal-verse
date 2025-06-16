using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Photon.Pun.Demo.Asteroids
{
    public class LobbyMainPanel : MonoBehaviourPunCallbacks
    {
        [Header("Login Panel")]
        public GameObject LoginPanel;

        public InputField PlayerNameInput;

        [Header("Avatar Panel")]
        public GameObject SelectionPanel;

        [Header("Create Room Panel")]
        public GameObject CreateRoomPanel;

        public InputField RoomNameInputField;
        public InputField MaxPlayersInputField;

        [Header("Join Random Room Panel")]
        public GameObject JoinRandomRoomPanel;

        [Header("Room List Panel")]
        public GameObject RoomListPanel;

        public GameObject RoomListContent;
        public GameObject RoomListEntryPrefab;

        [Header("Inside Room Panel")]
        public GameObject InsideRoomPanel;

        public Button StartGameButton;
        public GameObject PlayerListEntryPrefab;

        private Dictionary<string, RoomInfo> cachedRoomList;
        private Dictionary<string, GameObject> roomListEntries;
        private Dictionary<int, GameObject> playerListEntries;
        private string selectedRoomType = "Okul"; // Varsayılan oyun sahnesi olsun

        #region UNITY

        public void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            cachedRoomList = new Dictionary<string, RoomInfo>();
            roomListEntries = new Dictionary<string, GameObject>();

            if (PlayerPrefs.HasKey("PlayerName"))
            {
                PlayerNameInput.text = PlayerPrefs.GetString("PlayerName");
            }
            else
            {
                PlayerNameInput.text = "Oyuncu " + Random.Range(1000, 10000);
            }

            bool hasName = PlayerPrefs.HasKey("PlayerName");
            bool hasRole = PlayerPrefs.HasKey("IsStudent");
            // bool hasRoom = PlayerPrefs.HasKey("SelectedRoomType");
            bool hasCharacter = PlayerPrefs.GetInt("HasSelectedCharacter", 0) == 1;

            if (hasName && hasRole && hasCharacter)
            {
                string playerName = PlayerPrefs.GetString("PlayerName");
                if (!string.IsNullOrEmpty(playerName))
                {
                    PhotonNetwork.LocalPlayer.NickName = playerName;
                    PhotonNetwork.ConnectUsingSettings();
                }
            }
            else
            {
                // Giriş eksikse hangi panel açılacak?
                if (!hasName || !hasRole)
                {
                    SetActivePanel(LoginPanel.name);
                }
                else if (!hasCharacter)
                {
                    SetActivePanel(InsideRoomPanel.name); // Avatar seçimi burada
                }
            }
}

            public void ResetPreferences()
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();

                if (PhotonNetwork.InRoom)
                {
                    // Odaya bağlıysa önce odadan çık
                    PhotonNetwork.LeaveRoom();
                    // LeaveRoom tamamlandıktan sonra OnLeftRoom callback'i çağrılır
                }
                else if (PhotonNetwork.IsConnected)
                {
                    // Bağlantı varsa direkt disconnect et
                    PhotonNetwork.Disconnect();
                }
                else
                {
                    // Bağlantı yoksa direkt sahneyi yeniden yükle
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

        #endregion

        #region PUN CALLBACKS

        public override void OnConnectedToMaster()
        {
            bool hasCharacter = PlayerPrefs.GetInt("HasSelectedCharacter", 0) == 1;
            // bool hasRoom = PlayerPrefs.HasKey("SelectedRoomType");

            if (hasCharacter)
            {
                // Tüm seçimler yapıldıysa doğrudan oda paneline geç
                this.SetActivePanel(InsideRoomPanel.name);
            }
            else
            {
                // Avatar seçimi yapılmamışsa avatar paneline yönlendir
                this.SetActivePanel(SelectionPanel.name);
            }
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            ClearRoomListView();

            UpdateCachedRoomList(roomList);
            UpdateRoomListView();
        }

        public override void OnJoinedLobby()
        {
            // whenever this joins a new lobby, clear any previous room lists
            cachedRoomList.Clear();
            ClearRoomListView();
        }

        // note: when a client joins / creates a room, OnLeftLobby does not get called, even if the client was in a lobby before
        public override void OnLeftLobby()
        {
            cachedRoomList.Clear();
            ClearRoomListView();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            SetActivePanel(SelectionPanel.name);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            SetActivePanel(SelectionPanel.name);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            string roomName = "Oda " + Random.Range(1000, 10000);

            RoomOptions options = new RoomOptions {MaxPlayers = 20};

            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        public override void OnJoinedRoom()
        {
            // joining (or entering) a room invalidates any cached lobby room list (even if LeaveLobby was not called due to just joining a room)
            cachedRoomList.Clear();


            SetActivePanel(InsideRoomPanel.name);

            if (playerListEntries == null)
            {
                playerListEntries = new Dictionary<int, GameObject>();
            }

            foreach (Player p in PhotonNetwork.PlayerList)
            {
                GameObject entry = Instantiate(PlayerListEntryPrefab);
                entry.transform.SetParent(InsideRoomPanel.transform);
                entry.transform.localScale = Vector3.one;
                entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);

                object isPlayerReady;
                if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_READY, out isPlayerReady))
                {
                    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool) isPlayerReady);
                }

                playerListEntries.Add(p.ActorNumber, entry);
            }

                StartGameButton.gameObject.SetActive(true);

            Hashtable props = new Hashtable
            {
                {AsteroidsGame.PLAYER_LOADED_LEVEL, false}
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnLeftRoom()
        {
            SetActivePanel(SelectionPanel.name);

            foreach (GameObject entry in playerListEntries.Values)
            {
                Destroy(entry.gameObject);
            }

            playerListEntries.Clear();
            playerListEntries = null;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            GameObject entry = Instantiate(PlayerListEntryPrefab);
            entry.transform.SetParent(InsideRoomPanel.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<PlayerListEntry>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

            playerListEntries.Add(newPlayer.ActorNumber, entry);

            StartGameButton.gameObject.SetActive(true);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Destroy(playerListEntries[otherPlayer.ActorNumber].gameObject);
            playerListEntries.Remove(otherPlayer.ActorNumber);

            StartGameButton.gameObject.SetActive(true);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
            {
                StartGameButton.gameObject.SetActive(true);
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (playerListEntries == null)
            {
                playerListEntries = new Dictionary<int, GameObject>();
            }

            GameObject entry;
            if (playerListEntries.TryGetValue(targetPlayer.ActorNumber, out entry))
            {
                object isPlayerReady;
                if (changedProps.TryGetValue(AsteroidsGame.PLAYER_READY, out isPlayerReady))
                {
                    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool) isPlayerReady);
                }
            }

            StartGameButton.gameObject.SetActive(true);
        }

        #endregion

        #region UI CALLBACKS

        public void OnBackButtonClicked()
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }

            SetActivePanel(SelectionPanel.name);
        }

        public void OnCreateRoomButtonClicked()
        {
            string roomName = RoomNameInputField.text;
            roomName = (roomName.Equals(string.Empty)) ? "Oda " + Random.Range(1000, 10000) : roomName;

            byte maxPlayers;
            byte.TryParse(MaxPlayersInputField.text, out maxPlayers);
            maxPlayers = (byte) Mathf.Clamp(maxPlayers, 2, 20);

            RoomOptions options = new RoomOptions {MaxPlayers = maxPlayers, PlayerTtl = 10000 };

            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        public void OnJoinRandomRoomButtonClicked()
        {
            SetActivePanel(JoinRandomRoomPanel.name);

            PhotonNetwork.JoinRandomRoom();
        }

        public void OnLeaveGameButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        public void OnLoginButtonClicked()
        {
            string playerName = PlayerNameInput.text;

            if (!playerName.Equals(""))
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
                PlayerPrefs.SetString("PlayerName", playerName);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.LogError("Oyuncu ismi geçerli değil.");
            }
        }

        public void OnRoomListButtonClicked()
        {
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }

            SetActivePanel(RoomListPanel.name);
        }

public void OnStartGameButtonClicked()
{
    PhotonNetwork.CurrentRoom.IsOpen = true;
    PhotonNetwork.CurrentRoom.IsVisible = true;

    switch (selectedRoomType)
    {
        case "Sinif":
            PhotonNetwork.LoadLevel("Sinif"); // Okul sahnesinin adını burada belirt
            break;
        case "Atolye":
            PhotonNetwork.LoadLevel("Atolye");
            break;
        case "Ofis":
            PhotonNetwork.LoadLevel("Ofis");
            break;
        case "Konferans":
            PhotonNetwork.LoadLevel("Konferans");
            break;
        default:
            PhotonNetwork.LoadLevel("Sinif"); // Varsayılan okul olsun
            break;
    }
}

// Oda Seçim Fonksiyonları
public void SelectSinifRoom()
{
    selectedRoomType = "Sinif";
    // PlayerPrefs.SetString("SelectedRoomType", selectedRoomType);
    // PlayerPrefs.Save();
}

public void SelectAtolyeRoom()
{
    selectedRoomType = "Atolye";
    // PlayerPrefs.SetString("SelectedRoomType", selectedRoomType);
    // PlayerPrefs.Save();
}

public void SelectKonferansRoom()
{
    selectedRoomType = "Konferans";
    // PlayerPrefs.SetString("SelectedRoomType", selectedRoomType);
    // PlayerPrefs.Save();
}

public void SelectOfisRoom()
{
    selectedRoomType = "Ofis";
    // PlayerPrefs.SetString("SelectedRoomType", selectedRoomType);
    // PlayerPrefs.Save();
}

        #endregion
        
        private void ClearRoomListView()
        {
            foreach (GameObject entry in roomListEntries.Values)
            {
                Destroy(entry.gameObject);
            }

            roomListEntries.Clear();
        }

        public void LocalPlayerPropertiesUpdated()
        {
            StartGameButton.gameObject.SetActive(true);
        }

        private void SetActivePanel(string activePanel)
        {
            LoginPanel.SetActive(activePanel.Equals(LoginPanel.name));
            SelectionPanel.SetActive(activePanel.Equals(SelectionPanel.name));
            CreateRoomPanel.SetActive(activePanel.Equals(CreateRoomPanel.name));
            JoinRandomRoomPanel.SetActive(activePanel.Equals(JoinRandomRoomPanel.name));
            RoomListPanel.SetActive(activePanel.Equals(RoomListPanel.name));    // UI should call OnRoomListButtonClicked() to activate this
            InsideRoomPanel.SetActive(activePanel.Equals(InsideRoomPanel.name));
        }

        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                // Remove room from cached room list if it got closed, became invisible or was marked as removed
                if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
                {
                    if (cachedRoomList.ContainsKey(info.Name))
                    {
                        cachedRoomList.Remove(info.Name);
                    }

                    continue;
                }

                // Update cached room info
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList[info.Name] = info;
                }
                // Add new room info to cache
                else
                {
                    cachedRoomList.Add(info.Name, info);
                }
            }
        }

        private void UpdateRoomListView()
        {
            foreach (RoomInfo info in cachedRoomList.Values)
            {
                GameObject entry = Instantiate(RoomListEntryPrefab);
                entry.transform.SetParent(RoomListContent.transform);
                entry.transform.localScale = Vector3.one;
                entry.GetComponent<RoomListEntry>().Initialize(info.Name, (byte)info.PlayerCount, info.MaxPlayers);

                roomListEntries.Add(info.Name, entry);
            }
        }
    }
}