using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class VidPlayer2 : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] string videoFileName;

    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        Debug.Log(videoPath);
        videoPlayer.url = videoPath;
    }

    public void PlayVideo()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
            photonView.RPC("SyncVideoState", RpcTarget.All, true);
        }
    }

    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            photonView.RPC("SyncVideoState", RpcTarget.All, false);
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

    [PunRPC]
    void SyncVideoTime(float startTime)
    {
        videoPlayer.time = startTime;
        videoPlayer.Play();
    }

    // PhotonView'ýn durumunu senkronize etmek için gerekli olan OnPhotonSerializeView iþlevi
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(videoPlayer.time);
            stream.SendNext(videoPlayer.isPlaying);
        }
        else
        {
            float startTime = (float)stream.ReceiveNext();
            bool isPlaying = (bool)stream.ReceiveNext();
            photonView.RPC("SyncVideoTime", RpcTarget.All, startTime);
            if (isPlaying)
            {
                videoPlayer.Play();
            }
        }
    }
}
