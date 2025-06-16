using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Photon.Pun;

public class VidPlayerLink3 : MonoBehaviourPun
{
    public List<string> videoUrls;
    private VideoPlayer videoPlayer;
    private int currentVideoIndex = 0;
    private bool isPlaying = false;
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoUrls.Count > 0)
        {
            currentVideoIndex = 0;
            videoPlayer.url = videoUrls[currentVideoIndex];
        }
    }

        public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

        public void PlayVideo()
        {
            if (videoPlayer != null && !videoPlayer.isPlaying)
            {
                isPlaying = true;
                photonView.RPC("SyncVideoState", RpcTarget.All, currentVideoIndex, true, videoPlayer.time);
                videoPlayer.Play();
            }
        }

        public void PauseVideo()
        {
            if (videoPlayer != null && videoPlayer.isPlaying)
            {
                isPlaying = false;
                photonView.RPC("SyncVideoState", RpcTarget.All, currentVideoIndex, false, videoPlayer.time);
                videoPlayer.Pause();
            }
        }


    public void ChangeVideo(int index)
    {
        if (index >= 0 && index < videoUrls.Count)
        {
            currentVideoIndex = index;
            isPlaying = true;
            photonView.RPC("SyncVideoState", RpcTarget.All, currentVideoIndex, isPlaying, 0.0);
            photonView.RPC("SyncVideoState", RpcTarget.All, currentVideoIndex, isPlaying, videoPlayer.time);
        }
    }

    [PunRPC]
    void SyncVideoState(int videoIndex, bool playState, double time)
    {
        if (videoIndex < videoUrls.Count)
        {
            // Sadece video değiştiyse URL güncelle
            if (currentVideoIndex != videoIndex || videoPlayer.url != videoUrls[videoIndex])
            {
                currentVideoIndex = videoIndex;
                videoPlayer.url = videoUrls[videoIndex];
            }

            isPlaying = playState;

            videoPlayer.time = time;

            if (isPlaying)
            {
                videoPlayer.Play();
            }
            else
            {
                videoPlayer.Pause();
            }
        }
    }
}