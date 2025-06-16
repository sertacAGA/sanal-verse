using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.Networking;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour, IPointerDownHandler, IOnEventCallback
{
    public RawImage output; // Resim gösterimi için RawImage
    private string playerName; // Oyuncu Adý
    private const byte ShareUploadedFilesEventCode = 1; // Photon RPC için Event Code

    void Start()
    {
        playerName = PhotonNetwork.LocalPlayer.NickName; // Oyuncu adýný al
        Debug.Log($"Oyuncu Adý: {playerName}");

        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        PhotonNetwork.AddCallbackTarget(this); // Photon callback için ekle
    }

    private void OnClick()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // WebGL için dosya seçici
            OpenFilePanelForWebGL();
        }
        else
        {
            var paths = StandaloneFileBrowser.OpenFilePanel("Dosya Seç", "",
                new[] { new ExtensionFilter("Görüntü Dosyalarý", "png", "jpg"),
                        new ExtensionFilter("Tüm Dosyalar", "*") }, true);

            if (paths.Length > 0)
            {
                output.texture = null;
                foreach (string selectedFilePath in paths)
                {
                    StartCoroutine(UploadFileToServer(selectedFilePath));
                }
            }
        }
    }

    // WebGL için FilePicker iþlevi
    private void OpenFilePanelForWebGL()
    {
        string filePickerScript = @"
            var input = document.createElement('input');
            input.type = 'file';
            input.accept = 'image/*';
            input.onchange = function(e) {
                var file = e.target.files[0];
                if(file) {
                    var reader = new FileReader();
                    reader.onload = function(event) {
                        var base64Data = event.target.result.split(',')[1];
                        SendFileToUnity(base64Data);
                    };
                    reader.readAsDataURL(file);
                }
            };
            input.click();
        ";

        Application.ExternalEval(filePickerScript);
    }

    // JavaScript'ten gelen base64 verisini Unity'ye gönderin
    private void SendFileToUnity(string base64Data)
    {
        StartCoroutine(UploadFileToServerFromBase64(base64Data));
    }

    // Standalone (PC, Mac, Linux) için dosya yükleme
    private IEnumerator UploadFileToServer(string filePath)
    {
        byte[] fileData = System.IO.File.ReadAllBytes(filePath);

        // Dosyayý server'a yüklemek için WWWForm oluþtur
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, System.IO.Path.GetFileName(filePath), "image/png");

        UnityWebRequest www = UnityWebRequest.Post("http://sanalverse.wuaze.com/wp-json/custom/v1/upload", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string fileUrl = www.downloadHandler.text;
            Debug.Log($"Dosya baþarýyla yüklendi: {fileUrl}");

            // Photon ile dosya URL'sini paylaþ
            ShareFileUrl(fileUrl);
        }
        else
        {
            Debug.LogError($"Dosya yükleme baþarýsýz: {www.error}");
        }
    }

    // Base64 verisi ile sunucuya dosya yükleme (WebGL)
    private IEnumerator UploadFileToServerFromBase64(string base64Data)
    {
        byte[] fileData = System.Convert.FromBase64String(base64Data);

        // Dosyayý server'a yüklemek için WWWForm oluþtur
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, "image.png", "image/png");

        UnityWebRequest www = UnityWebRequest.Post("http://sanalverse.wuaze.com/wp-json/custom/v1/upload", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string fileUrl = www.downloadHandler.text;
            Debug.Log($"Dosya baþarýyla yüklendi: {fileUrl}");

            // Photon ile dosya URL'sini paylaþ
            ShareFileUrl(fileUrl);
        }
        else
        {
            Debug.LogError($"Dosya yükleme baþarýsýz: {www.error}");
        }
    }

    // Photon ile dosya URL'sini paylaþma
    private void ShareFileUrl(string fileUrl)
    {
        object[] content = new object[] { fileUrl };
        PhotonNetwork.RaiseEvent(ShareUploadedFilesEventCode, content, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
    }

    // URL'deki resmi yükle ve göster
    private IEnumerator LoadTextureFromUrl(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            output.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        else
        {
            Debug.LogError($"Resim yüklenemedi: {www.error}");
        }
    }

    // Photon event callback'ini dinleyin
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == ShareUploadedFilesEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            string fileUrl = (string)data[0];
            Debug.Log($"Diðer oyuncudan resim URL'si alýndý: {fileUrl}");

            // URL'deki resmi yükle ve göster
            StartCoroutine(LoadTextureFromUrl(fileUrl));
        }
    }

    // OnPointerDown metodunu ekleyin
    public void OnPointerDown(PointerEventData eventData)
    {
        // Burada butona týklanma olayýný iþleyebilirsiniz.
        Debug.Log("Pointer Down detected!");
    }

    void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this); // Callback'ý kaldýr
    }
}
