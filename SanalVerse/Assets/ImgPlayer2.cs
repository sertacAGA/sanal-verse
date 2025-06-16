using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImgPlayer2 : MonoBehaviour
{
    [SerializeField] string imageUrl = "https://tto.iste.edu.tr/content/photos/4/5a4b7f57f10e7.jpg"; // Replace with your image URL
    [SerializeField] GameObject imageGameObject; // Reference to the GameObject where the image will be displayed
    [SerializeField] RawImage rawImage; // Reference to the RawImage component on the GameObject

    private void Start()
    {
        if (!rawImage)
        {
            Debug.LogError("No RawImage component found!");
            return;
        }

        StartCoroutine(LoadImage());
    }

    IEnumerator LoadImage()
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