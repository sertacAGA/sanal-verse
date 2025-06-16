using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Video;

public class ImgPlayer3 : MonoBehaviour
{
    [SerializeField] RawImage rawImage; // Resmi gösterecek RawImage bileşeni
    [SerializeField] VideoPlayer videoPlayer; // Videoyu oynatacak VideoPlayer bileşeni

    private void Start()
    {
        if (!rawImage)
        {
            Debug.LogError("No RawImage component found!");
            return;
        }

        if (!videoPlayer)
        {
            Debug.LogError("No VideoPlayer component found!");
            return;
        }

        // PlayerPrefs'ten linkleri al
        string imageUrl = PlayerPrefs.GetString("ImageLink", "");
        string videoUrl = PlayerPrefs.GetString("VideoLink", "");

        // Resim linkini kontrol et ve yükle
        if (!string.IsNullOrEmpty(imageUrl))
        {
            StartCoroutine(LoadImage(imageUrl));
        }
        else
        {
            Debug.LogError("No ImageLink found in PlayerPrefs!");
        }

        // Video linkini kontrol et ve yükle
        if (!string.IsNullOrEmpty(videoUrl))
        {
            videoPlayer.url = videoUrl;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("No VideoLink found in PlayerPrefs!");
        }
    }

    IEnumerator LoadImage(string imageUrl)
    {
        using (WWW www = new WWW(imageUrl))
        {
            yield return www;

            if (www.isDone)
            {
                if (string.IsNullOrEmpty(www.error))
                {
                    Texture2D texture = www.texture;
                    rawImage.texture = texture;
                }
                else
                {
                    Debug.LogError("Failed to download image: " + www.error);
                }
            }
        }
    }
}
