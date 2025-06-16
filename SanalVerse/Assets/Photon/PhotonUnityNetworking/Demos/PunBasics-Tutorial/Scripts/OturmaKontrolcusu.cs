using UnityEngine;
using Photon.Pun;

public class OturmaKontrolcusu : MonoBehaviourPunCallbacks
{
    private Animator anim;
    private bool isSitting = false;
    private bool isWalking = false;
    private RaycastHit hitInfo;

    // Kamera hareket bileşeni referansı
    private KameraHareket kameraHareket;

    private void Start()
    {
        anim = GetComponent<Animator>();
        kameraHareket = GetComponent<KameraHareket>();

        // Başlangıçta kamera hareket bileşenini devre dışı bırakın
        if (kameraHareket != null)
        {
            kameraHareket.enabled = false;
        }
    }

    private void Update()
    {
        if (photonView.IsMine) // Sadece yerel oyuncu bu kontrolleri işlesin
        {
            if (isSitting)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Oturma animasyonunu durdur
                    anim.SetBool("IsWalking", true);
                    isSitting = false;

                    // Karakterin yönünü sandalyeden uzaklaştır
                    Vector3 newDirection = transform.forward;
                    newDirection.y = 0f;
                    transform.forward = newDirection;

                    // Kamera hareket bileşenini devre dışı bırakın
                    if (kameraHareket != null)
                    {
                        kameraHareket.enabled = false;
                    }

                    // Oturma işlemi diğer oyunculara iletilmeli
                    photonView.RPC("SetIsWalkingRPC", RpcTarget.All, true);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E)) // Sol tıklandığında
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider.CompareTag("Chair"))
                        {
                            // Sandalyeye tıklandığında oturma animasyonunu başlat
                            anim.SetBool("IsSitting", true);
                            isSitting = true;
                            transform.position = hitInfo.point; // Karakteri sandalyeye yerleştir

                            // Karakterin yönünü sandalyeye doğru çevir
                            Vector3 newDirection = hitInfo.transform.forward;
                            newDirection.y = 0f;
                            transform.forward = newDirection;

                            // Kamera hareket bileşenini aktif hale getirin
                            if (kameraHareket != null)
                            {
                                kameraHareket.enabled = true;
                            }

                            // Oturma işlemi diğer oyunculara iletilmeli
                            photonView.RPC("SetIsSittingRPC", RpcTarget.All, true);
                        }
                    }
                }
            }
        }
    }

    [PunRPC]
    private void SetIsSittingRPC(bool value)
    {
        isSitting = value;
        anim.SetBool("IsSitting", value);

        // Kamera hareket bileşeninin durumunu güncelle
        if (kameraHareket != null)
        {
            kameraHareket.enabled = value;
        }
    }

    [PunRPC]
    private void SetIsWalkingRPC(bool value)
    {
        isWalking = value;
        anim.SetBool("IsWalking", value);
    }
}
