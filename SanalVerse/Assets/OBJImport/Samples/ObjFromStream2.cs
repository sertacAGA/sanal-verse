using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Photon.Pun;

public class ObjFromStream2 : MonoBehaviourPun
{
    private List<string> modelUrls = new List<string> {
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/At/At_Modeli_Kaplamali.obj",
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/Pokemon/Pokemon.obj",
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/Ayi.obj"
    };

    private List<string> textureUrls = new List<string> {
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/At/default_material-color.png",
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/Pokemon/Final_Pokemon_Diffuse.jpg",
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/Ayi_kaplama.png"
    };

    private int currentModelIndex = 0;
    private List<GameObject> models = new List<GameObject>(); 
    private GameObject currentModel;

    void Start()
    {
        StartCoroutine(LoadAllModels()); 
    }

    IEnumerator LoadAllModels()
    {
        for (int i = 0; i < modelUrls.Count; i++)
        {
            UnityWebRequest www = UnityWebRequest.Get(modelUrls[i]);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text));
                GameObject model = new OBJLoader().Load(textStream);

                model.transform.position = new Vector3(14, 1, 22);
                model.transform.localScale = new Vector3(1f, 1f, 1f);
                model.transform.rotation = Quaternion.Euler(0, 180, 0);

                Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
                if (renderers.Length > 0)
                {
                    Texture2D texture = null;
                    yield return StartCoroutine(LoadTextureAsync(textureUrls[i], (tex) => texture = tex));

                    if (texture != null)
                    {
                        foreach (Renderer renderer in renderers)
                        {
                            Material newMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                            newMaterial.mainTexture = texture;
                            renderer.material = newMaterial;
                        }
                    }
                    else
                    {
                        Debug.LogError("Kaplama yüklenemedi: " + textureUrls[i]);
                    }
                }

                model.SetActive(false); 
                models.Add(model); 
                PhotonNetwork.Instantiate(model.name, model.transform.position, model.transform.rotation, 0); // Modeli ağda görünür hale getir
            }
            else
            {
                Debug.LogError("Model yüklenemedi: " + www.error);
            }
        }

        if (models.Count > 0)
        {
            currentModel = models[0];
            currentModel.SetActive(true);
        }
    }

    IEnumerator LoadTextureAsync(string url, System.Action<Texture2D> callback)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            callback(texture);
        }
        else
        {
            Debug.LogError("Kaplama yüklenemedi: " + www.error);
            callback(null);
        }
    }

    [PunRPC]
    void LoadModel(int index)
    {
        if (index < 0 || index >= models.Count) return;

        // Mevcut modeli gizle
        if (currentModel != null) currentModel.SetActive(false);

        // Yeni modeli göster
        currentModel = models[index];
        currentModel.SetActive(true);
    }

    public void NextModel()
    {
        if (models.Count == 0) return;

        currentModelIndex = (currentModelIndex + 1) % models.Count;
        photonView.RPC("LoadModel", RpcTarget.All, currentModelIndex); // RPC ile tüm oyunculara yeni modeli yükle
    }

    public void PreviousModel()
    {
        if (models.Count == 0) return;

        currentModelIndex--;
        if (currentModelIndex < 0)
        {
            currentModelIndex = models.Count - 1;
        }
        photonView.RPC("LoadModel", RpcTarget.All, currentModelIndex); // RPC ile tüm oyunculara yeni modeli yükle
    }
}
