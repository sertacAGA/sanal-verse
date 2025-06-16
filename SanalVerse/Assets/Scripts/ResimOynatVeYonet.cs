using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Networking;
using Photon.Pun.Demo.PunBasics;
using System;

public class ResimOynatVeYonet : MonoBehaviourPunCallbacks
{
    public RawImage imageDisplay;
    public Button nextButton;
    public Button prevButton;

    private List<Texture2D> loadedTextures = new List<Texture2D>();
    private int index = 0;

    [Header("Sunum Yönetimi")]
    public List<string> imageUrls; // Resim baðlantýlarý
    public List<InputField> linkInputs; // Paneldeki InputField'lar
    public GameObject linkEditorPanel; // Link düzenleme paneli

    private PlayerAnimatorManager localPlayerMovementScript; // Yerel oyuncunun hareket scripti

    private void Start()
    {
        StartCoroutine(LoadImagesFromLinks());

        nextButton.onClick.AddListener(() => OnNextButtonPressed());
        prevButton.onClick.AddListener(() => OnPrevButtonPressed());

        // Yerel oyuncunun hareket scriptini bul
        foreach (var player in FindObjectsOfType<PlayerAnimatorManager>())
        {
            if (player.photonView.IsMine)
            {
                localPlayerMovementScript = player;
                break;
            }
        }
    }

    IEnumerator LoadImagesFromLinks()
    {
        loadedTextures.Clear();

        foreach (var url in imageUrls)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);
                    loadedTextures.Add(texture);
                }
                else
                {
                    Debug.LogError("Resim yüklenemedi: " + request.error);
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
            Debug.LogWarning("Yüklenecek resim bulunamadý!");
        }
    }

    public void OpenLinkEditor()
    {
        linkEditorPanel.SetActive(true);

        // Hareket scriptini devre dýþý býrak
        if (localPlayerMovementScript != null)
        {
            localPlayerMovementScript.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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

        // Hareket scriptini tekrar etkinleþtir
        if (localPlayerMovementScript != null)
        {
            localPlayerMovementScript.enabled = true;
        }
    }

    public void SaveLinks()
    {
        imageUrls.Clear(); // Mevcut listeyi temizle

        foreach (var input in linkInputs)
        {
            // Eðer input boþ deðilse ve geçerli bir URL ise iþleme al
            if (!string.IsNullOrEmpty(input.text) && IsValidURL(input.text))
            {
                imageUrls.Add(input.text);
            }
            else if (string.IsNullOrEmpty(input.text))
            {
                Debug.LogWarning("Boþ bir InputField tespit edildi, atlanýyor.");
            }
            else
            {
                Debug.LogError("Geçersiz URL: " + input.text);
            }
        }

        // Güncellenen URL'leri tüm oyunculara gönder
        photonView.RPC("UpdateImageLinks", RpcTarget.AllBuffered, imageUrls.ToArray());
    }

    // URL'nin geçerli olup olmadýðýný kontrol eden yardýmcý fonksiyon
    private bool IsValidURL(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }


    [PunRPC]
    public void UpdateImageLinks(string[] newLinks)
    {
        imageUrls = new List<string>(newLinks);

        StartCoroutine(LoadImagesFromLinks());
    }

    public void OnNextButtonPressed()
    {
        photonView.RPC("NextImageRPC", RpcTarget.AllBuffered);
    }

    public void OnPrevButtonPressed()
    {
        photonView.RPC("PrevImageRPC", RpcTarget.AllBuffered);
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
}
