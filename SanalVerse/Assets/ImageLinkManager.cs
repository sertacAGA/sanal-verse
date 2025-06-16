using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon.Pun;

public class ImageLinkManager : MonoBehaviourPunCallbacks
{
    public ImgPlayer4 imgPlayer; // ImgPlayer4 script'ine referans
    public GameObject linkEditorPanel; // Link düzenleme paneli
    public List<InputField> linkInputs; // Her bir resim için bir InputField

    private void Start()
    {
        UpdateLinkInputs();
    }

    public void OpenLinkEditor()
    {
        linkEditorPanel.SetActive(true);
        UpdateLinkInputs();
    }

    public void CloseLinkEditor()
    {
        linkEditorPanel.SetActive(false);
    }

    private void UpdateLinkInputs()
    {
        // InputField'ları resim linkleriyle doldur
        for (int i = 0; i < linkInputs.Count; i++)
        {
            if (i < imgPlayer.imageUrls.Count)
            {
                linkInputs[i].text = imgPlayer.imageUrls[i];
            }
        }
    }

    public void SaveLinks()
    {
        // InputField'lardaki linkleri al ve listeye kaydet
        List<string> newLinks = new List<string>();
        foreach (var input in linkInputs)
        {
            newLinks.Add(input.text);
        }

        imgPlayer.imageUrls = newLinks;

        // Photon ile diğer oyunculara linkleri güncelle
        imgPlayer.photonView.RPC("UpdateImageLinks", RpcTarget.AllBuffered, newLinks.ToArray());
    }
}
