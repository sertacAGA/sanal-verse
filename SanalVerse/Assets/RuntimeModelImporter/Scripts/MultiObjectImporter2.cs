using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using AsImpL; // AsImpL namespace eklenmeli

public class MultiObjectImporter2 : MonoBehaviourPun
{
    [Tooltip("Models to load on startup")]
    public List<ModelImportInfo> objectsList = new List<ModelImportInfo>();

    [Tooltip("Default import options")]
    public ImportOptions defaultImportOptions = new ImportOptions();

    [SerializeField]
    private PathSettings pathSettings = null;

    private GameObject[] loadedModels;
    private int currentModelIndex = 0;

    public string RootPath
    {
        get
        {
            return pathSettings != null ? pathSettings.RootPath : "";
        }
    }

    // Kaplama URL'lerini manuel eklemek için
    public List<string> textureUrls = new List<string> {
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/At/default_material-color.png",
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/Pokemon/Final_Pokemon_Diffuse.jpg",
        "https://chatress.github.io/SanalVerse-Multiplayer-School-Simulator/3D/Ayi_kaplama.png"
    };

    void Start()
    {
        loadedModels = new GameObject[objectsList.Count];
        StartCoroutine(LoadAllModels());
    }

    IEnumerator LoadAllModels()
    {
        for (int i = 0; i < objectsList.Count; i++)
        {
            string objName = objectsList[i].name;
            string filePath = RootPath + objectsList[i].path;
            ImportOptions options = defaultImportOptions;

            // Modeli asenkron olarak yükle
            yield return ImportModelAsync(objName, filePath, options, (loadedModel) =>
            {
                loadedModels[i] = loadedModel;

                // İlk modeli aktif bırak, diğerlerini devre dışı bırak
                loadedModel.SetActive(i == 0);

                // Kaplamayı uygula
                StartCoroutine(LoadTextureAsync(textureUrls[i], (texture) =>
                {
                    if (texture != null)
                    {
                        Renderer[] renderers = loadedModel.GetComponentsInChildren<Renderer>();
                        foreach (Renderer renderer in renderers)
                        {
                            Material newMaterial = new Material(Shader.Find("Standard"));
                            newMaterial.mainTexture = texture;
                            renderer.material = newMaterial;
                        }
                    }
                }));
            });
        }
    }

    IEnumerator ImportModelAsync(string objName, string filePath, ImportOptions options, System.Action<GameObject> onModelLoaded)
    {
        // AsImpL'den model yükleme işlemi
        yield return new WaitForSeconds(1); // Bu kısmı AsImpL'den gelen yükleme yöntemine göre değiştirmelisiniz.

        // Model yüklendiğinde onModelLoaded çağrılır.
        onModelLoaded?.Invoke(new GameObject(objName)); // Burada geçici bir GameObject oluşturduk. Asıl modeli burada yüklemelisiniz.
    }

    IEnumerator LoadTextureAsync(string url, System.Action<Texture2D> callback)
    {
        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            Texture2D texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(www);
            callback(texture);
        }
        else
        {
            Debug.LogError("Kaplama yüklenemedi: " + www.error);
            callback(null);
        }
    }

    // İleri butonu için
    public void NextModel()
    {
        loadedModels[currentModelIndex].SetActive(false);
        currentModelIndex = (currentModelIndex + 1) % loadedModels.Length;
        loadedModels[currentModelIndex].SetActive(true);
    }

    // Geri butonu için
    public void PreviousModel()
    {
        loadedModels[currentModelIndex].SetActive(false);
        currentModelIndex = (currentModelIndex - 1 + loadedModels.Length) % loadedModels.Length;
        loadedModels[currentModelIndex].SetActive(true);
    }
}
