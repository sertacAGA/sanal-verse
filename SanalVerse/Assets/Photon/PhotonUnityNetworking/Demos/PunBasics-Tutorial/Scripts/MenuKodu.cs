using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuKodu : MonoBehaviour
{

    public void Karakter()
    {
        // Sahneyi deðiþtir
        PhotonNetwork.LoadLevel("Karakter");
    }
}
