using UnityEngine;
using Photon.Pun;

public class KameraHareket2 : MonoBehaviourPunCallbacks
{
    public float sensitivity = 2f; // Dönme hassasiyeti

    private bool isControllingCamera = false; // Sadece kendi oyuncusunun kamerasını kontrol ettiğini belirten değişken

    void Start()
    {
        if (photonView.IsMine)
        {
            isControllingCamera = true; // Sadece kendi oyuncusunun kamerasını kontrol edeceğiz
        }
    }

    void Update()
    {
        if (!isControllingCamera)
        {
            return; // Diğer oyuncuların kameralarını kontrol etmiyoruz
        }

        float mouseX = Input.GetAxis("Mouse X"); // Fare yatay hareketi
        float mouseY = Input.GetAxis("Mouse Y"); // Fare dikey hareketi

        // Yatay dönüş için kamerayı etrafında döndür
        transform.Rotate(Vector3.up * mouseX * sensitivity, Space.World);

        // Dikey dönüş için kamerayı kendi etrafında döndür
        transform.Rotate(Vector3.right * -mouseY * sensitivity);

        // Karakterin hareketini etkilemeyecek şekilde kamerayı döndürdükten sonra
        // karakterinizi sabit tutmak için bir şey yapmanıza gerek yok
    }
}
