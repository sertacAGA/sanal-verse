using UnityEngine;

namespace YourNamespace
{
    public class PlayerAnimatorManager2 : MonoBehaviour
    {
        public float moveSpeed = 5.0f;      // Karakterin hareket hızı

        private void Update()
        {
            // Karakterin ileri/geri ve yatay hareketini al
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Hareket vektörünü oluştur
            Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

            // Hareketi dünya koordinatlarına göre uygula
            transform.Translate(movement, Space.World);
        }
    }
}
