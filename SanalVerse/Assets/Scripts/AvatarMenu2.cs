using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarMenu2 : MonoBehaviour
{
    public List<GameObject> maleStudentAvatars;
    public List<GameObject> femaleStudentAvatars;
    public List<GameObject> maleTeacherAvatars;
    public List<GameObject> femaleTeacherAvatars;

    public GameObject sunumYukleButton;
    public GameObject videoYukleButton;
    public GameObject nesneYukleButton;

    private void Start()
    {
        string selectedGender = PlayerPrefs.GetString("SelectedGender", "Male");
        int selectedCharacterIndex = PlayerPrefs.GetInt("CharacterSelected", 0);
        bool isStudent = PlayerPrefs.GetInt("IsStudent", 1) == 1;

        GameObject avatarPrefab = null;

        if (isStudent)
        {
            // Öðrenci avatarlarýný kontrol edin
            if (selectedGender == "Male" && selectedCharacterIndex >= 0 && selectedCharacterIndex < maleStudentAvatars.Count)
            {
                avatarPrefab = maleStudentAvatars[selectedCharacterIndex];
            }
            else if (selectedGender == "Female" && selectedCharacterIndex >= 0 && selectedCharacterIndex < femaleStudentAvatars.Count)
            {
                avatarPrefab = femaleStudentAvatars[selectedCharacterIndex];
            }
        }
        else
        {
            // Öðretmen avatarlarýný kontrol edin
            if (selectedGender == "Male" && selectedCharacterIndex >= 0 && selectedCharacterIndex < maleTeacherAvatars.Count)
            {
                avatarPrefab = maleTeacherAvatars[selectedCharacterIndex];
            }
            else if (selectedGender == "Female" && selectedCharacterIndex >= 0 && selectedCharacterIndex < femaleTeacherAvatars.Count)
            {
                avatarPrefab = femaleTeacherAvatars[selectedCharacterIndex];
            }
        }

        if (avatarPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(0, 1.5f, 0); // Y = 1.5 sahneye göre ayarlanabilir
            PhotonNetwork.Instantiate(avatarPrefab.name, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid character selection.");
        }

        // Eðer öðrenci seçildiyse, sunum/video/3B nesne düðmelerini gizle
        UpdateUI(isStudent);
    }

    private void UpdateUI(bool isStudent)
    {
        if (isStudent)
        {
            sunumYukleButton.SetActive(false);
            videoYukleButton.SetActive(false);
            nesneYukleButton.SetActive(false);
        }
    }
}
