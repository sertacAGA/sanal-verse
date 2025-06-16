using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class ResimOynatVeYonet : MonoBehaviourPunCallbacks
{
    public RawImage imageDisplay;
    public Button nextButton;
    public Button prevButton;

    private List<Texture2D> loadedTextures = new List<Texture2D>();
    private int index = 0;

    [Header("Sunum Yönetimi")]
    public List<string> imageUrls;
    public List<InputField> linkInputs; // TMP yerine standart InputField kullanımı
    public GameObject linkEditorPanel;

    private void Start()
    {
        StartCoroutine(LoadImagesFromLinks());

        nextButton.onClick.AddListener(() => OnNextButtonPressed());
        prevButton.onClick.AddListener(() => OnPrevButtonPressed());
    }

    IEnumerator LoadImagesFromLinks()
    {
        loadedTextures.Clear();

        foreach (var url in imageUrls)
        {
            using (WWW www = new WWW(url))
            {
                yield return www;

                if (www.isDone && string.IsNullOrEmpty(www.error))
                {
                    Texture2D texture = www.texture;
                    loadedTextures.Add(texture);
                }
                else
                {
                    Debug.LogError("Resim yüklenemedi: " + www.error);
                }
            }
        }

        if (loadedTextures.Count > 0)
        {
            index = 0;
            imageDisplay.texture = loadedTextures[index];
        }
        else
        {
            Debug.LogWarning("Yüklenecek resim bulunamadı!");
        }
    }

    public void ToggleRight()
    {
        if (loadedTextures.Count == 0) return;

        index++;
        if (index >= loadedTextures.Count)
            index = 0;

        imageDisplay.texture = loadedTextures[index];
    }

    public void ToggleLeft()
    {
        if (loadedTextures.Count == 0) return;

        index--;
        if (index < 0)
            index = loadedTextures.Count - 1;

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

    public void OpenLinkEditor()
    {
        linkEditorPanel.SetActive(true);

        for (int i = 0; i < linkInputs.Count; i++)
        {
            if (i < imageUrls.Count)
            {
                linkInputs[i].text = imageUrls[i];
            }
        }
    }

    public void CloseLinkEditor()
    {
        linkEditorPanel.SetActive(false);
    }

    public void SaveLinks()
    {
        imageUrls.Clear();

        foreach (var input in linkInputs)
        {
            imageUrls.Add(input.text);
        }

        photonView.RPC("UpdateImageLinks", RpcTarget.AllBuffered, imageUrls.ToArray());
    }

    [PunRPC]
    public void UpdateImageLinks(string[] newLinks)
    {
        imageUrls = new List<string>(newLinks);

        StartCoroutine(LoadImagesFromLinks());
    }
}
