using UnityEngine;
using Photon.Pun;

public class OturmaKontrolcusu2 : MonoBehaviourPunCallbacks
{
    private Animator anim;
    private bool isSitting = false;
    private RaycastHit hitInfo; // Tıklanan nesneyi saklamak için

    public GameObject cameraInsideCharacter; // Karakterin içindeki kamera objesi

    private void Start()
    {
        anim = GetComponent<Animator>();
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

                    // Oturma işlemi diğer oyunculara iletilmeli
                    photonView.RPC("SetIsWalkingRPC", RpcTarget.All, true);

                    // Karakterin içindeki kamerayı deaktif et
                    if (cameraInsideCharacter != null)
                    {
                        cameraInsideCharacter.SetActive(false);
                    }
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

                            // Oturma işlemi diğer oyunculara iletilmeli
                            photonView.RPC("SetIsSittingRPC", RpcTarget.All, true);

                            // Karakterin içindeki kamerayı aktif et
                            if (cameraInsideCharacter != null)
                            {
                                cameraInsideCharacter.SetActive(true);
                            }
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
    }

    [PunRPC]
    private void SetIsWalkingRPC(bool value)
    {
        anim.SetBool("IsWalking", value);
    }
}
