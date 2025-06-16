using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Chairsit3 : MonoBehaviourPun
{
    public GameObject oturDugmesi;
    public GameObject kalkDugmesi;
    private OturmaKontrolcusu oturmaKontrolcusu;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            oturmaKontrolcusu = playerObject.GetComponent<OturmaKontrolcusu>();
        }

        kalkDugmesi.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && photonView.IsMine)
        {
            if (!oturmaKontrolcusu.isSitting) 
            {
                oturDugmesi.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && photonView.IsMine)
        {
            oturDugmesi.SetActive(false);
            kalkDugmesi.SetActive(false);
        }
    }

    public void Otur()
    {
        if (photonView.IsMine && oturmaKontrolcusu != null && !oturmaKontrolcusu.isSitting)
        {
            Transform chairTransform = this.transform;
            oturmaKontrolcusu.transform.position = chairTransform.position + new Vector3(0, 0.1f, -0.2f);
            oturmaKontrolcusu.transform.rotation = chairTransform.rotation;

            oturmaKontrolcusu.photonView.RPC("SetIsSittingRPC", RpcTarget.AllBuffered, true);

            oturDugmesi.SetActive(false);
            kalkDugmesi.SetActive(true);
        }
    }

    public void Kalk()
    {
        if (photonView.IsMine && oturmaKontrolcusu != null && oturmaKontrolcusu.isSitting)
        {
            oturmaKontrolcusu.photonView.RPC("SetIsSittingRPC", RpcTarget.AllBuffered, false);

            kalkDugmesi.SetActive(false);
            oturDugmesi.SetActive(true);
        }
    }
}
