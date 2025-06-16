using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Photon.Pun;
using Photon.Realtime;

public class VideoSync : MonoBehaviourPunCallbacks, IPunObservable
{
    private VideoPlayer player;
    private double syncTime;
    private bool syncPlay;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (player.isPlaying)
            {
                syncTime = player.time;
                syncPlay = true;
            }
            else
            {
                syncPlay = false;
            }
        }
        else
        {
            if (syncPlay)
            {
                player.time = syncTime;
                player.Play();
            }
            else
            {
                player.Pause();
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player.time);
            stream.SendNext(player.isPlaying);
        }
        else
        {
            syncTime = (double)stream.ReceiveNext();
            syncPlay = (bool)stream.ReceiveNext();
        }
    }
}
