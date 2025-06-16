using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class AvatarMenu : MonoBehaviour
{
    public GameObject avatarPrefab1;
    public GameObject avatarPrefab2;
    public GameObject avatarPrefab3;
    public GameObject avatarPrefab4;

    public void SelectAvatar1()
    {
        // Sahneyi deðiþtir
        // SceneManager.LoadScene("PunBasics-Room for 1");
        // PhotonNetwork.LoadLevel("Oda");
        // Instantiate the avatar prefab
        PhotonNetwork.Instantiate(avatarPrefab1.name, Vector3.zero, Quaternion.identity);
    }

    public void SelectAvatar2()
    {
        // Sahneyi deðiþtir
        // SceneManager.LoadScene("PunBasics-Room for 1");
        // PhotonNetwork.LoadLevel("Oda");
        // Instantiate the avatar prefab
        PhotonNetwork.Instantiate(avatarPrefab2.name, Vector3.zero, Quaternion.identity);
    }
        public void SelectAvatar3()
    {
        // Sahneyi deðiþtir
        // SceneManager.LoadScene("PunBasics-Room for 1");
        // PhotonNetwork.LoadLevel("Oda");
        // Instantiate the avatar prefab
        PhotonNetwork.Instantiate(avatarPrefab3.name, Vector3.zero, Quaternion.identity);
    }
        public void SelectAvatar4()
    {
        // Sahneyi deðiþtir
        // SceneManager.LoadScene("PunBasics-Room for 1");
        // PhotonNetwork.LoadLevel("Oda");
        // Instantiate the avatar prefab
        PhotonNetwork.Instantiate(avatarPrefab4.name, Vector3.zero, Quaternion.identity);
    }
}
