using UnityEngine;
using Photon.Pun;

public class KameraHareket : MonoBehaviourPunCallbacks
{
    public float sensitivity = 2f; // D�nme hassasiyeti

    private bool isControllingCamera = false; // Sadece kendi oyuncusunun kameras�n� kontrol etti�ini belirten de�i�ken

    void Start()
    {
        if (photonView.IsMine)
        {
            isControllingCamera = true; // Sadece kendi oyuncusunun kameras�n� kontrol edece�iz
        }
    }

    void Update()
    {
        if (!isControllingCamera)
        {
            return; // Di�er oyuncular�n kameralar�n� kontrol etmiyoruz
        }

        float mouseX = Input.GetAxis("Mouse X"); // Fare yatay hareketi
        float mouseY = Input.GetAxis("Mouse Y"); // Fare dikey hareketi

        transform.Rotate(Vector3.up * mouseX * sensitivity, Space.World); // Yatay d�nme
        transform.Rotate(Vector3.right * -mouseY * sensitivity); // Dikey d�nme
    }
}
