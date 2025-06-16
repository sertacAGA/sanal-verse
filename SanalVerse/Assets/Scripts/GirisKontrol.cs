using UnityEngine;
using UnityEngine.SceneManagement;

public class GirisKontrol : MonoBehaviour
{
    public GameObject loginPanel;

    public void OyunaBasla()
    {
        loginPanel.SetActive(true); // Login paneli görünür hale gelir
    }

    public void KayitOl()
    {
        SceneManager.LoadScene("Kayit");
    }   
}
