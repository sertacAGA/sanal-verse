using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DugmeKodu : MonoBehaviourPunCallbacks
{
    public bool isOn;
    public GameObject objectToToggle;

    public void ToggleObject()
    {
        photonView.RPC("ToggleObjectRPC", RpcTarget.AllBuffered, !isOn);
    }

    [PunRPC]
    private void ToggleObjectRPC(bool state)
    {
        isOn = state;
        objectToToggle.SetActive(isOn);
    }
}