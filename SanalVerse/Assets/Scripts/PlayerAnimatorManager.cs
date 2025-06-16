using UnityEngine;
using UnityEngine.InputSystem; // Yeni Input System için
using Photon.Pun; // Photon PUN için

namespace Photon.Pun.Demo.PunBasics
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {
        #region Private Fields

        [SerializeField]
        private float moveSpeed = 1.0f; // Hareket hýzý
        [SerializeField]
        private float rotationSpeed = 10.0f; // Dönüþ hýzý

        private Animator animator; // Animatör
        private CharacterController characterController; // Karakter kontrolcüsü

        // Joystick input referansý
        [SerializeField] private InputActionReference moveAction;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();

            // Input sistemini aktif hale getir
            moveAction.action.Enable();
        }

        private void Update()
        {
            // Photon PUN: Sadece kendi oyuncumuzun kontrolüne izin ver
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (!animator)
            {
                return;
            }

            // Left stick input (X: Sað/Sol, Y: Ýleri/Geri)
            Vector2 input = moveAction.action.ReadValue<Vector2>();

            // Hareket vektörünü hesapla
            Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;

            // Animator parametrelerini güncelle
            animator.SetFloat("Speed", moveDirection.magnitude); // Hýz
            animator.SetFloat("Direction", input.x); // Yön

            // Karakteri hareket ettir
            if (moveDirection.magnitude > 0.1f)
            {
                // Karakteri döndür (hareket yönüne bakacak þekilde)
                Vector3 localMoveDirection = transform.TransformDirection(moveDirection);
                float targetAngle = Mathf.Atan2(localMoveDirection.x, localMoveDirection.z) * Mathf.Rad2Deg;
                float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);

                // Karakteri hareket ettir
                characterController.Move(localMoveDirection * moveSpeed * Time.deltaTime);
            }
        }

        private void OnDisable()
        {
            // Input sistemini devre dýþý býrak
            moveAction.action.Disable();
        }

        #endregion
    }
}
