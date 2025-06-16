using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    public class CameraWork : MonoBehaviour
    {
        [Header("Camera Follow")]
        public float distance = 7.0f;
        public float height = 1.5f;
        public float smoothSpeed = 0.125f;
        public Vector3 centerOffset = new Vector3(0f, 1.6f, 0f);
        public bool followOnStart = true;

        private Transform cameraTransform;
        private bool isFollowing;

        [Header("Look Settings")]
        [SerializeField] private float sensitivity = 100f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;
        [SerializeField] private float characterTurnFollowSpeed = 5f;

        private float manualYaw = 0f;
        private float manualPitch = 0f;

        private bool userTouchingRightSide = false;
        private bool characterIsMoving = false;

        void Start()
        {
            cameraTransform = Camera.main.transform;

            if (followOnStart)
            {
                StartFollowing();
            }
        }

        private void LateUpdate()
        {
            if (isFollowing && cameraTransform != null)
            {
                Follow();
            }

#if UNITY_STANDALONE || UNITY_EDITOR
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            manualYaw += mouseX;
            manualPitch -= mouseY;
#elif UNITY_ANDROID || UNITY_IOS
            userTouchingRightSide = false;
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved && touch.position.x > Screen.width * 0.5f)
                {
                    float deltaX = touch.deltaPosition.x * sensitivity * 0.02f * Time.deltaTime;
                    float deltaY = touch.deltaPosition.y * sensitivity * 0.02f * Time.deltaTime;

                    manualYaw += deltaX;
                    manualPitch -= deltaY;
                    userTouchingRightSide = true;
                }
            }
#endif

            // Karakter hareket ediyorsa ve kullanıcı sağ taraftan dokunmuyorsa → kamerayı karaktere döndür
            if (!userTouchingRightSide)
            {
                // Basit karakter hareket kontrolü: ileri-geri tuşu veya joystick hareketi gibi algılanabilir
                characterIsMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

                if (characterIsMoving)
                {
                    float targetYaw = transform.eulerAngles.y;
                    manualYaw = Mathf.LerpAngle(manualYaw, targetYaw, characterTurnFollowSpeed * Time.deltaTime);
                }
            }

            manualPitch = Mathf.Clamp(manualPitch, minPitch, maxPitch);
            cameraTransform.rotation = Quaternion.Euler(manualPitch, manualYaw, 0f);
        }

        public void StartFollowing()
        {
            isFollowing = true;
            cameraTransform = Camera.main.transform;
            Cut();
        }

        private void Follow()
        {
            Vector3 targetPosition = transform.position + centerOffset;
            Vector3 desiredPosition = targetPosition - cameraTransform.forward * distance + Vector3.up * height;
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed);
            cameraTransform.position = smoothedPosition;
        }

        private void Cut()
        {
            Vector3 targetPosition = transform.position + centerOffset;
            cameraTransform.position = targetPosition - cameraTransform.forward * distance + Vector3.up * height;
        }
    }
}