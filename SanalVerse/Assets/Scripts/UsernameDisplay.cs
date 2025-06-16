using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UsernameDisplay : MonoBehaviour
{
    public Text playerNameText;

    void Start()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
        {
            playerNameText.text = PhotonNetwork.LocalPlayer.NickName;
        }
    }
}
