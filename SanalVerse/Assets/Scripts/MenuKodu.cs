using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Eklendi

public class MenuKodu : MonoBehaviour
{
    public void AnaMenu()
    {
        StartCoroutine(GeriDon());
    }

    private IEnumerator GeriDon()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            while (PhotonNetwork.InRoom)
            {
                yield return null; // Oda terk edilene kadar bekle
            }
        }
        PhotonNetwork.LoadLevel("Menu");
    }
}