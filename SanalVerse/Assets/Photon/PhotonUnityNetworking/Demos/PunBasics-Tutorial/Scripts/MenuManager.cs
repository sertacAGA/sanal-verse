using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class MenuManager : MonoBehaviour
{
    // Yonetim sahnesine gitmek için
    public void GoToYonetimScene()
    {
        SceneManager.LoadScene("Yonetim"); // Yonetim adlý sahneye geçiþ yapar
    }

    // Menu sahnesine geri dönmek için
    public void GoToMenuScene()
    {
        SceneManager.LoadScene("Menu"); // Menu adlý sahneye geçiþ yapar
    }
}
