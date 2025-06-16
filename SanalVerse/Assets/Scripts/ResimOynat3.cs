using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using UnityEngine.Networking;

public class ResimOynat3 : MonoBehaviourPunCallbacks
{
    public RawImage imageDisplay; // Resmi göstermek için bir RawImage
    public Button nextButton;
    public Button prevButton;
    public GameObject loadingSpinner; // Yükleme animasyonu

    private List<Texture2D> loadedTextures = new List<Texture2D>(); // Yüklenen resimlerin listesi
    private int index = 0; // Hangi resmin gösterildiðini takip eder

    void Start()
    {
        // Uygulamanýn kök dizinindeki "Sunum" klasöründe, oyuncu adýyla eþleþen klasörü bul
        StartCoroutine(LoadImagesFromPlayerFolder());

        nextButton.onClick.AddListener(() => OnNextButtonPressed());
        prevButton.onClick.AddListener(() => OnPrevButtonPressed());
    }

    private IEnumerator LoadImagesFromPlayerFolder()
    {
        // Oyuncunun adýný al
        string playerName = PhotonNetwork.NickName;
        string folderPath = Path.Combine(Application.dataPath, "Sunum", playerName); // Oyuncu adýna göre klasörü bul

        if (!Directory.Exists(folderPath))
        {
            Debug.LogError(playerName + " adlý oyuncuya ait klasör bulunamadý: " + folderPath);
            yield break;
        }

        // Yükleme iþlemine baþlamadan önce loading spinner'ý göster
        loadingSpinner.SetActive(true);

        // Sadece .png ve .jpg dosyalarýný al
        string[] files = Directory.GetFiles(folderPath, "*.*");
        foreach (var filePath in files)
        {
            if (filePath.EndsWith(".png") || filePath.EndsWith(".jpg"))
            {
                string url = "file:///" + filePath; // Yerel dosyaya eriþmek için "file:///" protokolü kullanýlýr
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        Texture2D texture = DownloadHandlerTexture.GetContent(www); // Resmi Texture2D'ye yükle
                        loadedTextures.Add(texture);
                    }
                    else
                    {
                        Debug.LogError("Resim yüklenemedi: " + www.error);
                    }
                }
            }
        }

        // Yükleme iþlemi tamamlandý
        loadingSpinner.SetActive(false); // Yükleme spinner'ýný gizle

        // Eðer hiç resim yüklenmezse uyarý ver
        if (loadedTextures.Count == 0)
        {
            Debug.LogError(playerName + " adlý oyuncunun klasöründe yüklenebilir resim bulunamadý!");
        }
        else
        {
            // Ýlk resmi ekrana göster
            index = 0;
            imageDisplay.texture = loadedTextures[index];
        }
    }

    public void ToggleLeft()
    {
        // Liste boþsa hiçbir þey yapma
        if (loadedTextures.Count == 0)
            return;

        // Bir önceki resme geç
        index--;
        if (index < 0)
            index = loadedTextures.Count - 1;

        // Resmi deðiþtir
        imageDisplay.texture = loadedTextures[index];
    }

    public void ToggleRight()
    {
        // Liste boþsa hiçbir þey yapma
        if (loadedTextures.Count == 0)
            return;

        // Bir sonraki resme geç
        index++;
        if (index >= loadedTextures.Count)
            index = 0;

        // Resmi deðiþtir
        imageDisplay.texture = loadedTextures[index];
    }

    [PunRPC]
    public void NextImageRPC()
    {
        ToggleRight();
    }

    [PunRPC]
    public void PrevImageRPC()
    {
        ToggleLeft();
    }

    public void OnNextButtonPressed()
    {
        photonView.RPC("NextImageRPC", RpcTarget.AllBuffered);
    }

    public void OnPrevButtonPressed()
    {
        photonView.RPC("PrevImageRPC", RpcTarget.AllBuffered);
    }
}
