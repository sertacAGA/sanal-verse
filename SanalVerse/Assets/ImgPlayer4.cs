using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon.Pun;

public class ImgPlayer4 : MonoBehaviourPunCallbacks
{
    public List<string> imageUrls; // Inspector'dan linkleri buraya ekleyebilirsiniz
    public List<RawImage> rawImages; // Tüm RawImage nesnelerinin referansları

    private void Start()
    {
        if (imageUrls.Count != rawImages.Count)
        {
            Debug.LogError("RawImage sayısı ile link sayısı eşleşmiyor!");
            return;
        }

        // Her bir RawImage için resim yükle
        for (int i = 0; i < rawImages.Count; i++)
        {
            StartCoroutine(LoadImage(rawImages[i], imageUrls[i]));
        }
    }

    IEnumerator LoadImage(RawImage rawImage, string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;

            if (www.isDone && string.IsNullOrEmpty(www.error))
            {
                Texture2D texture = www.texture;
                rawImage.texture = texture;
            }
            else
            {
                Debug.LogError("Resim yüklenemedi: " + www.error);
            }
        }
    }

    [PunRPC]
    public void UpdateImageLinks(string[] newLinks)
    {
        imageUrls = new List<string>(newLinks);

        // Linkler güncellendiğinde resimleri yeniden yükle
        for (int i = 0; i < rawImages.Count; i++)
        {
            StartCoroutine(LoadImage(rawImages[i], imageUrls[i]));
        }
    }
}
