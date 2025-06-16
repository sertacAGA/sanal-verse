using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using Dummiesman;
using System.IO;
using System.Text;
using ExitGames.Client.Photon;

public class ObjFromStream4 : MonoBehaviourPun, IOnEventCallback
{
    [SerializeField] private GameObject panel;
    [SerializeField] private InputField modelUrlInput;
    [SerializeField] private InputField textureUrlInput;
    [SerializeField] private Button loadModelButton;
    [SerializeField] private Button nextModelButton;
    [SerializeField] private Button previousModelButton;

    private List<ModelData> models = new List<ModelData>();
    private int currentModelIndex = -1;
    private GameObject currentModel;

    private const byte LoadModelEventCode = 1; // Photon Custom Event Code

    [System.Serializable]
    public class ModelData
    {
        public string modelUrl;
        public string textureUrl;
    }

    private void Start()
    {
        panel.SetActive(false);

        loadModelButton.onClick.AddListener(AddModel);
        nextModelButton.onClick.AddListener(NextModel);
        previousModelButton.onClick.AddListener(PreviousModel);
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void AddModel()
    {
        string modelUrl = modelUrlInput.text;
        string textureUrl = textureUrlInput.text;

        if (!string.IsNullOrEmpty(modelUrl) && !string.IsNullOrEmpty(textureUrl))
        {
            models.Add(new ModelData { modelUrl = modelUrl, textureUrl = textureUrl });
            currentModelIndex = models.Count - 1;

            StartCoroutine(LoadModel(modelUrl, textureUrl));

            object[] eventData = { modelUrl, textureUrl, currentModelIndex };
            PhotonNetwork.RaiseEvent(LoadModelEventCode, eventData, RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }

    public void NextModel()
    {
        if (models.Count > 0)
        {
            currentModel.SetActive(false);
            currentModelIndex = (currentModelIndex + 1) % models.Count;
            ModelData nextModelData = models[currentModelIndex];
            StartCoroutine(LoadModel(nextModelData.modelUrl, nextModelData.textureUrl));

            object[] eventData = { nextModelData.modelUrl, nextModelData.textureUrl, currentModelIndex };
            PhotonNetwork.RaiseEvent(LoadModelEventCode, eventData, RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }

    public void PreviousModel()
    {
        if (models.Count > 0)
        {
            currentModel.SetActive(false);
            currentModelIndex = (currentModelIndex - 1 + models.Count) % models.Count;
            ModelData prevModelData = models[currentModelIndex];
            StartCoroutine(LoadModel(prevModelData.modelUrl, prevModelData.textureUrl));

            object[] eventData = { prevModelData.modelUrl, prevModelData.textureUrl, currentModelIndex };
            PhotonNetwork.RaiseEvent(LoadModelEventCode, eventData, RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }

    IEnumerator LoadModel(string modelUrl, string textureUrl)
    {
        UnityWebRequest modelRequest = UnityWebRequest.Get(modelUrl);
        yield return modelRequest.SendWebRequest();

        if (modelRequest.result == UnityWebRequest.Result.Success)
        {
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(modelRequest.downloadHandler.text));
            GameObject model = new OBJLoader().Load(textStream);

            model.transform.position = new Vector3(3, 1, 13);
            model.transform.localScale = new Vector3(1f, 1f, 1f);
            model.transform.rotation = Quaternion.Euler(0, 180, 0);

            UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(textureUrl);
            yield return textureRequest.SendWebRequest();

            if (textureRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
                Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                    material.mainTexture = texture;
                    renderer.material = material;
                }
            }
            else
            {
                Debug.LogError("Kaplama yüklenemedi: " + textureRequest.error);
            }

            if (currentModel != null)
                Destroy(currentModel);

            currentModel = model;
        }
        else
        {
            Debug.LogError("Model yüklenemedi: " + modelRequest.error);
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == LoadModelEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            string modelUrl = (string)data[0];
            string textureUrl = (string)data[1];
            int index = (int)data[2];

            if (index >= 0 && index < models.Count)
            {
                models[index] = new ModelData { modelUrl = modelUrl, textureUrl = textureUrl };
            }
            else
            {
                models.Add(new ModelData { modelUrl = modelUrl, textureUrl = textureUrl });
            }

            currentModelIndex = index;
            StartCoroutine(LoadModel(modelUrl, textureUrl));
        }
    }
}
