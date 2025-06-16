using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

public class ObjFromStream3 : MonoBehaviourPun
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
        StartCoroutine(LoadAllModels()); // Load all models at startup
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
                
                // Set position, scale, and rotation
                model.transform.position = new Vector3(14, 1, 22);
                model.transform.localScale = new Vector3(1f, 1f, 1f);
                model.transform.rotation = Quaternion.Euler(0, 180, 0);

                // Load and apply texture
                Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
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
                    Debug.LogError("Failed to load texture: " + textureUrls[i]);
                }

                model.SetActive(i == 0); // Activate the first model
                models.Add(model); // Store model in list
            }
            else
            {
                Debug.LogError("Failed to load model: " + www.error);
            }
        }

        // Show the first model
        if (models.Count > 0)
        {
            currentModel = models[0];
            currentModel.SetActive(true);
        }
    }

    IEnumerator LoadTextureAsync(string url, System.Action<Texture2D> callback)
    {
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(url);
        yield return textureRequest.SendWebRequest();

        if (textureRequest.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
            callback(texture);
        }
        else
        {
            Debug.LogError("Failed to load texture: " + textureRequest.error);
            callback(null);
        }
    }

    public void NextModel()
    {
        if (models.Count == 0) return;

        // Hide the current model
        currentModel.SetActive(false);

        // Increment the index and wrap around if necessary
        currentModelIndex = (currentModelIndex + 1) % models.Count;
        currentModel = models[currentModelIndex];
        currentModel.SetActive(true);
    }

    public void PreviousModel()
    {
        if (models.Count == 0) return;

        // Hide the current model
        currentModel.SetActive(false);

        // Decrement the index and wrap around if necessary
        currentModelIndex = (currentModelIndex - 1 + models.Count) % models.Count;
        currentModel = models[currentModelIndex];
        currentModel.SetActive(true);
    }
}
