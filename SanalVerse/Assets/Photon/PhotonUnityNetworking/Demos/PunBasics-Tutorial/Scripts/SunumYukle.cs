using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;

public class SunumYukle : MonoBehaviourPunCallbacks
{
    public RawImage imageDisplay; // Resmi gösterecek RawImage bileþeni
    private List<Texture2D> loadedTextures = new List<Texture2D>(); // Yüklenen resimlerin listesi

    // OpenFilePicker fonksiyonunu WebGL'de çaðýrýyoruz
    public void OpenFilePicker()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // JavaScript'ten dosya seçimini tetikle
        Application.ExternalEval("UploadFile('OnFileSelected');");
#else
        Debug.LogWarning("JavaScript köprüsü sadece WebGL'de çalýþýr.");
#endif
    }

    // JavaScript'ten gönderilen base64 verisini alýr ve resmi yükler
    public void OnFileSelected(string base64Data)
    {
        // Base64 verisini iþleyip Texture2D'ye dönüþtür
        byte[] imageBytes = System.Convert.FromBase64String(base64Data.Substring(base64Data.IndexOf(",") + 1));
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        // Yüklenen resmi listeye ekle
        loadedTextures.Add(texture);

        // Ýlk resmi göster (isteðe baðlý)
        if (loadedTextures.Count == 1)
        {
            imageDisplay.texture = loadedTextures[0];
        }

        // Photon üzerinden diðer oyunculara resmi gönder
        photonView.RPC("SyncImage", RpcTarget.AllBuffered, base64Data);
    }

    // Resmi senkronize etmek için RPC fonksiyonu
    [PunRPC]
    public void SyncImage(string base64Data)
    {
        byte[] imageBytes = System.Convert.FromBase64String(base64Data.Substring(base64Data.IndexOf(",") + 1));
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        // Resmi RawImage bileþenine ata
        imageDisplay.texture = texture;
    }

    // Resimler arasýnda geçiþ yapmaya yarayan metodlar
    public void ShowNextImage()
    {
        if (loadedTextures.Count == 0) return;

        // Bir sonraki resme geç
        int nextIndex = (loadedTextures.IndexOf((Texture2D)imageDisplay.texture) + 1) % loadedTextures.Count;
        imageDisplay.texture = loadedTextures[nextIndex];

        // Photon üzerinden diðer oyunculara geçiþi bildir
        photonView.RPC("SyncNextImage", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void SyncNextImage()
    {
        if (loadedTextures.Count == 0) return;

        // Bir sonraki resme geç
        int nextIndex = (loadedTextures.IndexOf((Texture2D)imageDisplay.texture) + 1) % loadedTextures.Count;
        imageDisplay.texture = loadedTextures[nextIndex];
    }

    public void ShowPreviousImage()
    {
        if (loadedTextures.Count == 0) return;

        // Bir önceki resme geç
        int prevIndex = (loadedTextures.IndexOf((Texture2D)imageDisplay.texture) - 1 + loadedTextures.Count) % loadedTextures.Count;
        imageDisplay.texture = loadedTextures[prevIndex];

        // Photon üzerinden diðer oyunculara geçiþi bildir
        photonView.RPC("SyncPreviousImage", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void SyncPreviousImage()
    {
        if (loadedTextures.Count == 0) return;

        // Bir önceki resme geç
        int prevIndex = (loadedTextures.IndexOf((Texture2D)imageDisplay.texture) - 1 + loadedTextures.Count) % loadedTextures.Count;
        imageDisplay.texture = loadedTextures[prevIndex];
    }
}
