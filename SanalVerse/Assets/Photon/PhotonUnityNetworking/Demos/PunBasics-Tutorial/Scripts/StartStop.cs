using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class StartStop : MonoBehaviourPunCallbacks, IPunObservable
{
    private VideoPlayer player;
    public Button button;
    public Sprite startSprite;
    public Sprite stopSprite;

    private bool isPlaying = false; // Eklediðimiz yeni deðiþken

    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    void Update()
    {

    }

    public void ChangeStartStop()
    {
        if (player.isPlaying == false)
        {
            player.Play();
            button.image.sprite = stopSprite;
            isPlaying = true; // Videonun oynayýp oynamadýðýný saklamak için deðiþkeni güncelledik
        }
        else
        {
            player.Pause();
            button.image.sprite = startSprite;
            isPlaying = false; // Videonun oynayýp oynamadýðýný saklamak için deðiþkeni güncelledik
        }

        photonView.RPC("SyncVideoState", RpcTarget.Others, isPlaying); // RPC iþlevini çaðýrdýk
    }

    // RPC iþlevimiz
    [PunRPC]
    void SyncVideoState(bool state)
    {
        if (state == true)
        {
            player.Play();
            button.image.sprite = stopSprite;
        }
        else
        {
            player.Pause();
            button.image.sprite = startSprite;
        }
    }

    // PhotonView'ýn durumunu senkronize etmek için gerekli olan OnPhotonSerializeView iþlevi
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isPlaying);
        }
        else
        {
            isPlaying = (bool)stream.ReceiveNext();
        }
    }
}
