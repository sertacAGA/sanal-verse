using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    /// <summary>
    /// Camera work. Follow a target and allow manual rotation with right mouse button.
    /// </summary>
    public class CameraWork2 : MonoBehaviour
    {
        [Tooltip("The distance in the local x-z plane to the target")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("The height we want the camera to be above the target")]
        [SerializeField]
        private float height = 3.0f;

        [Tooltip("Allow the camera to be offset vertically from the target, for example giving more view of the scenery and less ground.")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [Tooltip("The Smoothing for the camera to follow the target")]
        [SerializeField]
        private float smoothSpeed = 0.125f;

        private Transform cameraTransform;
        private Transform targetTransform;
        private bool isFollowing = false;

        private Vector3 cameraOffset = Vector3.zero;
        private Vector3 targetAngles;
        private Vector3 followAngles;
        private float angularSpeed = 10f;

        private void Start()
        {
            // Cache the main camera transform
            cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (isFollowing)
            {
                Follow();
            }
            else
            {
                HandleRotation();
            }
        }

        public void OnStartFollowing(Transform target)
        {
            targetTransform = target;
            isFollowing = true;
            Cut();
        }

        private void Follow()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position + targetTransform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);

            cameraTransform.LookAt(targetTransform.position + centerOffset);
        }

        private void Cut()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = targetTransform.position + targetTransform.TransformVector(cameraOffset);

            cameraTransform.LookAt(targetTransform.position + centerOffset);
        }

        private void HandleRotation()
        {
            // Rotate camera based on right mouse button
            if (Input.GetMouseButton(1)) // Right mouse button
            {
                targetAngles.y += Input.GetAxis("Mouse X") * angularSpeed;
                targetAngles.x -= Input.GetAxis("Mouse Y") * angularSpeed;
                targetAngles.x = Mathf.Clamp(targetAngles.x, -60f, 60f); // Clamp vertical angle to avoid flipping

                followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref targetAngles, 0.1f);
                cameraTransform.localRotation = Quaternion.Euler(followAngles.x, followAngles.y, 0);
            }
        }
    }
}
