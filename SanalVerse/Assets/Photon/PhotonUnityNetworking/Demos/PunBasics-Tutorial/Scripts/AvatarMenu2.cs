using System.Collections.Generic; // Bu satýrý ekleyin
using Photon.Pun;
using UnityEngine;

public class AvatarMenu2 : MonoBehaviour
{
    public List<GameObject> maleStudentAvatars;
    public List<GameObject> femaleStudentAvatars;
    public List<GameObject> maleTeacherAvatars;
    public List<GameObject> femaleTeacherAvatars;

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
            PhotonNetwork.Instantiate(avatarPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid character selection.");
        }
    }
}
