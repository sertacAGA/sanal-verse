using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {
        #region Private Fields

        [SerializeField]
        private float directionDampTime = 0.25f;
        [SerializeField]
        private float moveSpeed = 2.0f;  // Hareket hýzý
        [SerializeField]
        private float rotationSpeed = 200.0f;  // Dönüþ hýzý
        private Animator animator;
        private CharacterController characterController;

        #endregion

        #region MonoBehaviour CallBacks

        void Start()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {

            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (!animator)
            {
                return;
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // Yön parametresini hesapla (blend tree için)
            float direction = 0;

            if (v > 0)  // Ýleri hareket
            {
                direction = 0;
            }
            else if (v < 0)  // Geri hareket
            {
                direction = -2;
            }
            else if (h > 0)  // Sað hareket
            {
                direction = 1;
            }
            else if (h < 0)  // Sol hareket
            {
                direction = -1;
            }

            // Animasyon parametrelerini ayarla
            animator.SetFloat("Speed", Mathf.Abs(h) + Mathf.Abs(v)); // Mutlak deðerler üzerinden hesapla
            animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);

            // Karakterin ileri ve geri hareketini düzenliyoruz
            Vector3 moveDir = transform.forward * v * moveSpeed * Time.deltaTime;
            characterController.Move(moveDir);

            // Karakterin saða sola dönmesini düzenliyoruz
            transform.Rotate(0, h * rotationSpeed * Time.deltaTime, 0);
        }

        #endregion
    }
}
