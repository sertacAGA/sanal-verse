using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ResimOynat : MonoBehaviourPunCallbacks
{
public List<Image> images;
public Button nextButton;
public Button prevButton;
private int index;

public void ToggleLeft()
{
    images[index].gameObject.SetActive(false);
    index--;
    if (index < 0)
        index = images.Count - 1;
    images[index].gameObject.SetActive(true);
}


public void ToggleRight()
{
    images[index].gameObject.SetActive(false);
    index++;
    if (index == images.Count)
        index = 0;
    images[index].gameObject.SetActive(true);
}


[PunRPC]
public void NextImageRPC()
{
    ToggleRight();
}

[PunRPC]
public void PrevImageRPC()
{
    ToggleLeft();
}

public void OnNextButtonPressed()
{
    photonView.RPC("NextImageRPC", RpcTarget.AllBuffered);
}

public void OnPrevButtonPressed()
{
    photonView.RPC("PrevImageRPC", RpcTarget.AllBuffered);
}
}