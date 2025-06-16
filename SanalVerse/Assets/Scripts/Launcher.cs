using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    #pragma warning disable 649

    public class Launcher : MonoBehaviourPunCallbacks
    {
        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;

        [Tooltip("The Ui Text to inform the user about the connection progress")]
        [SerializeField]
        private Text feedbackText;

        [Tooltip("The maximum number of players per room")]
        [SerializeField]
        private byte maxPlayersPerRoom = 20;

        [Tooltip("The UI Loader Anime")]
        [SerializeField]
        private LoaderAnime loaderAnime;

        private bool isConnecting;
        private string gameVersion = "1";

        // Seçilen sahne adı
        private string selectedSceneName;

        void Awake()
        {
            if (loaderAnime == null)
            {
                Debug.LogError("<Color=Red><b>Missing</b></Color> loaderAnime Reference.", this);
            }

            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// Sahneyi belirleyip bağlantı başlatma metodu.
        /// </summary>
        /// <param name="sceneName">Yüklenecek sahnenin adı</param>
        public void SelectSceneAndConnect(string sceneName)
        {
            selectedSceneName = sceneName; // Tıklanan düğmeye göre sahne adı
            Connect();
        }

        public void Connect()
        {
            feedbackText.text = "";
            isConnecting = true;

            if (loaderAnime != null)
            {
                loaderAnime.StartLoaderAnimation();
            }

            if (PhotonNetwork.IsConnected)
            {
                LogFeedback("Odaya Giriliyor...");
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                LogFeedback("Bağlanıyor...");
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = this.gameVersion;
            }
        }

        void LogFeedback(string message)
        {
            if (feedbackText == null) return;

            feedbackText.text += System.Environment.NewLine + message;
        }

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                LogFeedback("OnConnectedToMaster: Odaya giriliyor...");
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Yeni oda oluşturuluyor");
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            LogFeedback("<Color=Red>OnDisconnected</Color> " + cause);
            loaderAnime.StopLoaderAnimation();
            isConnecting = false;
            controlPanel.SetActive(true);
        }

        public override void OnJoinedRoom()
        {
            LogFeedback("<Color=Green>OnJoinedRoom</Color> with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)");
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log($"'{selectedSceneName}' sahnesi yükleniyor.");
                PhotonNetwork.LoadLevel(selectedSceneName);
            }
        }
    }
}
