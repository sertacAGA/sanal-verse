using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class AvatarMenu3 : MonoBehaviour
{
    public GameObject[] avatarPrefabs; // Karakter prefab'larýnýn bir dizisi

    private void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("CharacterSelected");
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < avatarPrefabs.Length)
        {
            // Seçilen karakteri instantiate et
            PhotonNetwork.Instantiate(avatarPrefabs[selectedCharacterIndex].name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid character index.");
        }
    }
}
