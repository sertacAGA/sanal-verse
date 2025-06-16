using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Chairsit2 : MonoBehaviourPun
{
    // public GameObject[] sittingPrefabs; // Oturan nesnesi prefab'larý
    public GameObject sittingPrefab1, sittingPrefab2, sittingPrefab3, sittingPrefab4;
    public GameObject intText, standText, MainCamera, Camera; // Mesaj metinleri
    public bool interactable, sitting;
    private string characterTag; // Oturan karakterin etiketi
    private GameObject playerObject; // Player objesinin referansý
    private GameObject sittingObject; // Player objesinin referansý

    public GameObject avatarPrefab1;
    public GameObject avatarPrefab2;
    public GameObject avatarPrefab3;
    public GameObject avatarPrefab4;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E) && !sitting)
        {
            intText.SetActive(false);
            MainCamera.SetActive(false);
            Camera.SetActive(true);

            // Karakterin etiketini kullanarak oturan nesnesini instantiate edin
            // GameObject sittingCharacter = Instantiate(sittingPrefabs[0], transform.position, transform.rotation);
            // characterTag = sittingCharacter.tag; // Oturan karakterin etiketini al
            // characterTag = playerObject.tag; // Oturan karakterin etiketini al

            playerObject = GameObject.FindGameObjectWithTag("ErkekOgretmen"); // Player etiketini uygun þekilde deðiþtirebilirsiniz

            if (playerObject != null)
            {
                PhotonNetwork.Destroy(playerObject); // Ayakta nesnesini yok et
                                                     // sittingPrefab1.SetActive(true);
                PhotonNetwork.Instantiate(sittingPrefab1.name, transform.position, transform.rotation);
                playerObject = null;
            }

            playerObject = GameObject.FindGameObjectWithTag("KadinOgretmen"); // Player etiketini uygun þekilde deðiþtirebilirsiniz

            if (playerObject != null)
            {
                PhotonNetwork.Destroy(playerObject); // Ayakta nesnesini yok et
                                                     // sittingPrefab2.SetActive(true);
                PhotonNetwork.Instantiate(sittingPrefab2.name, transform.position, transform.rotation);
                playerObject = null;
            }

            playerObject = GameObject.FindGameObjectWithTag("Erkek");

            if (playerObject != null)
            {
                PhotonNetwork.Destroy(playerObject); // Ayakta nesnesini yok et
                                                     // sittingPrefab3.SetActive(true);
                PhotonNetwork.Instantiate(sittingPrefab3.name, transform.position, transform.rotation);
                playerObject = null;
            }

            playerObject = GameObject.FindGameObjectWithTag("Kiz");

            if (playerObject != null)
            {
                PhotonNetwork.Destroy(playerObject); // Ayakta nesnesini yok et
                                                     // sittingPrefab4.SetActive(true);
                PhotonNetwork.Instantiate(sittingPrefab4.name, transform.position, transform.rotation);
                playerObject = null;
            }

            photonView.RPC("ToggleSittingState", RpcTarget.All, true);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && sitting)
        {
            // GameObject[] sittingCharacter = GameObject.FindGameObjectsWithTag(characterTag);
            MainCamera.SetActive(true);
            Camera.SetActive(false);
            intText.SetActive(false);

            // foreach (GameObject character in sittingCharacter)
            // {
            //      PhotonNetwork.Destroy(character); // Oturan nesneleri yok et
            // }

            sittingObject = GameObject.FindGameObjectWithTag("ErkekHoca"); // Player etiketini uygun þekilde deðiþtirebilirsiniz

            if (sittingObject != null)
            {
                PhotonNetwork.Instantiate(avatarPrefab1.name, transform.position, transform.rotation);
                // sittingPrefab1.SetActive(false);
                PhotonNetwork.Destroy(sittingObject); // Ayakta nesnesini yok et
                sittingObject = null;
            }
            sittingObject = GameObject.FindGameObjectWithTag("KadinHoca"); // Player etiketini uygun þekilde deðiþtirebilirsiniz

            if (sittingObject != null)
            {
                PhotonNetwork.Instantiate(avatarPrefab2.name, transform.position, transform.rotation);
                // sittingPrefab2.SetActive(false);
                PhotonNetwork.Destroy(sittingObject); // Ayakta nesnesini yok et
                sittingObject = null;
            }
            sittingObject = GameObject.FindGameObjectWithTag("ErkekOtur"); // Player etiketini uygun þekilde deðiþtirebilirsiniz

            if (sittingObject != null)
            {
                PhotonNetwork.Instantiate(avatarPrefab3.name, transform.position, transform.rotation);
                // sittingPrefab3.SetActive(false);
                PhotonNetwork.Destroy(sittingObject); // Ayakta nesnesini yok et
                sittingObject = null;
            }
            sittingObject = GameObject.FindGameObjectWithTag("KizOtur"); // Player etiketini uygun þekilde deðiþtirebilirsiniz

            if (sittingObject != null)
            {
                PhotonNetwork.Instantiate(avatarPrefab4.name, transform.position, transform.rotation);
                // sittingPrefab4.SetActive(false);
                PhotonNetwork.Destroy(sittingObject); // Ayakta nesnesini yok et
                sittingObject = null;
            }

            photonView.RPC("ToggleSittingState", RpcTarget.All, false);
        }
    }

    [PunRPC]
    void ToggleSittingState(bool sitState)
    {
        sitting = sitState;
        standText.SetActive(sitState);
        interactable = !sitState;
    }
}
