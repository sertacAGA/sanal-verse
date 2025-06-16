using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    public class CharacterMovement : MonoBehaviour
    {
        public float moveSpeed = 5.0f;      // Karakterin hareket hızı

        private void Update()
        {
            // Karakterin ileri/geri hareketini al
            float vertical = Input.GetAxis("Vertical");

            // Hareket vektörünü oluştur
            Vector3 movement = new Vector3(0, 0, vertical) * moveSpeed * Time.deltaTime;

            // Hareketi yerel koordinatlara göre uygula
            transform.Translate(movement, Space.Self);
        }
    }
}
