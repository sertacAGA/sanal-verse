using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class VidPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] string videoFileName;

    private VideoPlayer videoPlayer;
    private bool isPlaying = false; // Eklediðimiz yeni deðiþken

    // Start is called before the first frame update
    void Start()
    {
        // PauseVideo();
    }

    public void PlayVideo()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            isPlaying = true; // Videonun oynayýp oynamadýðýný saklamak için deðiþkeni güncelledik
        }
        else
        {
            Debug.LogError("VideoPlayer component not found!");
        }
        photonView.RPC("SyncVideoState", RpcTarget.Others, isPlaying); // RPC iþlevini çaðýrdýk
    }

    public void PauseVideo()
    {
        if (videoPlayer)
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                isPlaying = false; // Videonun oynayýp oynamadýðýný saklamak için deðiþkeni güncelledik
            }
            else
            {
                videoPlayer.Play();
                isPlaying = true; // Videonun oynayýp oynamadýðýný saklamak için deðiþkeni güncelledik
            }
        }
    }

    // RPC iþlevimiz
    [PunRPC]
    void SyncVideoState(bool state)
    {
        if (state == true)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
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
