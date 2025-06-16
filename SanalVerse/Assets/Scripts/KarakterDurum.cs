using UnityEngine;
using UnityEngine.UI;

public class KarakterDurum : MonoBehaviour
{
    public Toggle studentToggle;
    public Toggle teacherToggle;

    public Button createRoomButton; // Oda oluþturma butonu
    public InputField classNameInput; // Sýnýf ismi giriþ alaný
    public InputField maxPlayersInput; // Maksimum oyuncu giriþ alaný

    public Button joinRoomButton; // "Sýnýfa Gir" butonu
    public Button otherRoomsButton; // "Diðer Sýnýflar" butonu

    void Start()
    {
        if (PlayerPrefs.HasKey("IsStudent"))
        {
            bool isStudent = PlayerPrefs.GetInt("IsStudent") == 1;
            studentToggle.isOn = isStudent;
            teacherToggle.isOn = !isStudent;
        }
        else
        {
            studentToggle.isOn = true;
            teacherToggle.isOn = false;
            PlayerPrefs.SetInt("IsStudent", 1);
        }

        // UI güncelle
        UpdateUI();
    }

    public void OnStudentToggle(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("IsStudent", 1);
            teacherToggle.isOn = false;
        }
        UpdateUI();
    }

    public void OnTeacherToggle(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("IsStudent", 0);
            studentToggle.isOn = false;
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        bool isStudent = PlayerPrefs.GetInt("IsStudent") == 1;

        // Öðrenci ise Create Room bölümü devre dýþý
        if (createRoomButton != null) createRoomButton.interactable = !isStudent;
        if (classNameInput != null) classNameInput.readOnly = isStudent;
        if (maxPlayersInput != null) maxPlayersInput.readOnly = isStudent;

        // "Sýnýfa Gir" ve "Diðer Sýnýflar" butonlarý her zaman açýk
        if (joinRoomButton != null) joinRoomButton.interactable = true;
        if (otherRoomsButton != null) otherRoomsButton.interactable = true;
    }
}
