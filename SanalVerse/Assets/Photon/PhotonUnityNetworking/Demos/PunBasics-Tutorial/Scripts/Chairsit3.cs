using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Chairsit3 : MonoBehaviourPun
{
    public GameObject intText, standText; // Mesaj metinleri
    public bool interactable, sitting;

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
}
