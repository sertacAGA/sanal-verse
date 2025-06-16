using UnityEngine;
using UnityEngine.UI;

public class KarakterDurum : MonoBehaviour
{
    public Toggle studentToggle;
    public Toggle teacherToggle;

    void Start()
    {
        // Önce kaydedilmiþ bir seçim var mý kontrol edin
        if (PlayerPrefs.HasKey("IsStudent"))
        {
            // Kaydedilmiþ deðeri yükleyin
            bool isStudent = PlayerPrefs.GetInt("IsStudent") == 1;
            studentToggle.isOn = isStudent;
            teacherToggle.isOn = !isStudent;
        }
        else
        {
            // Varsayýlan olarak öðrenci seçili olsun
            studentToggle.isOn = true;
            teacherToggle.isOn = false;
            PlayerPrefs.SetInt("IsStudent", 1);
        }
    }

    public void OnStudentToggle(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("IsStudent", 1); // Öðrenci olarak ayarlayýn
            teacherToggle.isOn = false;
        }
    }

    public void OnTeacherToggle(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("IsStudent", 0); // Öðretmen olarak ayarlayýn
            studentToggle.isOn = false;
        }
    }
}
