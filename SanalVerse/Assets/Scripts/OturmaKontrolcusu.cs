using UnityEngine;
using Photon.Pun;

public class OturmaKontrolcusu : MonoBehaviourPunCallbacks
{
    public Animator anim;
    public bool isSitting = false;
    public KameraHareket kameraHareket;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (kameraHareket != null)
        {
            kameraHareket.enabled = false;
        }
    }

    [PunRPC]
    public void SetIsSittingRPC(bool value)
    {
        isSitting = value;
        anim.SetBool("IsSitting", value);

        if (value) // **OTURMA DURUMU**
        {
            anim.SetBool("IsWalking", false); // Yürümeyi kapat
            transform.position += new Vector3(0, 0.1f, -0.2f);
        }
        else // **KALKMA DURUMU**
        {
            anim.SetBool("IsSitting", false); // Oturma animasyonunu kapat
            anim.SetBool("IsWalking", true);  // Kalkýnca yürümeyi baþlat
            transform.position += new Vector3(0, 0, 0.5f); // Karakteri biraz ileri al, sandalyede kalmasýn
        }
    }
}
