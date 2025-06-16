using UnityEngine;
using Photon.Pun;

public class SandalyeKodu2 : MonoBehaviourPun
{
    public GameObject Kamera; // İlk karakteriniz
    public GameObject Dugme; // İlk karakteriniz
    public GameObject MainKamera; // İlk karakteriniz

    public GameObject ErkekHoca; // Oturan karakter nesnesi
    public GameObject KadinHoca; // Oturan karakter nesnesi
    public GameObject Erkek; // Oturan karakter nesnesi
    public GameObject Kiz; // Oturan karakter nesnesi

    public GameObject erkekOgretmenPrefab; // Sahneye doğurulacak prefab
    public GameObject kadinOgretmenPrefab; // Sahneye doğurulacak prefab
    public GameObject erkekPrefab; // Sahneye doğurulacak prefab
    public GameObject kizPrefab; // Sahneye doğurulacak prefab

    private GameObject oturanKarakter; // Player objesinin referansı
    private GameObject tiklayanKarakter; // Tıklayan karakter referansı

    private bool nesneAktif = true;

    private void OnMouseDown()
    {
        Kamera.SetActive(true); // Karakter 1'i etkinleştirin
        Dugme.SetActive(true); // Karakter 2'i etkinleştirin

        // ErkekHoca etiketine sahip nesneyi bul
        tiklayanKarakter = GameObject.FindGameObjectWithTag("ErkekHoca"); // Player etiketini uygun şekilde değiştirebilirsiniz

        // Player nesnesi varsa, nesneyi gizle
        if (tiklayanKarakter != null)
        {
            {
                ErkekHoca.SetActive(true); // Oturan oyuncuyu göster
                tiklayanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
        }

        // KadinHoca etiketine sahip nesneyi bul
        tiklayanKarakter = GameObject.FindGameObjectWithTag("KadinHoca"); // Player etiketini uygun şekilde değiştirebilirsiniz

        // Player nesnesi varsa, nesneyi gizle
        if (tiklayanKarakter != null)
        {
            {
                KadinHoca.SetActive(true); // Oturan oyuncuyu göster
                tiklayanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
        }

        // Erkek etiketine sahip nesneyi bul
        tiklayanKarakter = GameObject.FindGameObjectWithTag("Erkek"); // Player etiketini uygun şekilde değiştirebilirsiniz

        // Player nesnesi varsa, nesneyi gizle
        if (tiklayanKarakter != null)
        {
            {
                Erkek.SetActive(true); // Oturan oyuncuyu göster
                tiklayanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
        }

        // Kiz etiketine sahip nesneyi bul
        tiklayanKarakter = GameObject.FindGameObjectWithTag("Kiz"); // Player etiketini uygun şekilde değiştirebilirsiniz

        // Player nesnesi varsa, nesneyi gizle
        if (tiklayanKarakter != null)
        {
            {
                Kiz.SetActive(true); // Oturan oyuncuyu göster
                tiklayanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
        }
    }

    // Eğer Player nesnesini tekrar göstermek isterseniz, aşağıdaki fonksiyonu çağırabilirsiniz
    public void OyuncuGoster()
    {
        Kamera.SetActive(false); // Karakter 1'i etkinleştirin
        MainKamera.SetActive(true); // Ana kamerayı aktive et
        Dugme.SetActive(false); // Karakter 2'i etkinleştirin

        // ErkekHoca etiketine sahip nesneyi bul
        oturanKarakter = GameObject.FindGameObjectWithTag("ErkekHoca"); // Player etiketini uygun şekilde değiştirebilirsiniz
        erkekOgretmenPrefab.SetActive(nesneAktif); // Tiklayan karakteri gizle

        // Player nesnesi varsa, nesneyi gizle
        if (oturanKarakter != null)
        {
            {
                oturanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
            nesneAktif = !nesneAktif;
        }

        // KadinHoca etiketine sahip nesneyi bul
        oturanKarakter = GameObject.FindGameObjectWithTag("KadinHoca"); // Player etiketini uygun şekilde değiştirebilirsiniz
        kadinOgretmenPrefab.SetActive(nesneAktif); // Tiklayan karakteri gizle

        // Player nesnesi varsa, nesneyi gizle
        if (oturanKarakter != null)
        {
            {
                oturanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
            nesneAktif = !nesneAktif;
        }

        // Erkek etiketine sahip nesneyi bul
        oturanKarakter = GameObject.FindGameObjectWithTag("Erkek"); // Player etiketini uygun şekilde değiştirebilirsiniz
        erkekPrefab.SetActive(nesneAktif); // Tiklayan karakteri gizle

        // Player nesnesi varsa, nesneyi gizle
        if (oturanKarakter != null)
        {
            {
                oturanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
            nesneAktif = !nesneAktif;
        }

        // Kiz etiketine sahip nesneyi bul
        oturanKarakter = GameObject.FindGameObjectWithTag("Kiz"); // Player etiketini uygun şekilde değiştirebilirsiniz
        kizPrefab.SetActive(nesneAktif); // Tiklayan karakteri gizle

        // Player nesnesi varsa, nesneyi gizle
        if (oturanKarakter != null)
        {
            {
                oturanKarakter.SetActive(false); // Tiklayan karakteri gizle
            }
        }
        else
        {
            Debug.Log("Player nesnesi bulunamadı.");
            nesneAktif = !nesneAktif;
        }
    }
}