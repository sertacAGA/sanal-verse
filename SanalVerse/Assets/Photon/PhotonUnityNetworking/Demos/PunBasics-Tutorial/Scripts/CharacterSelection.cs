using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];
        // diziyi modeller ile doldurun
        for (int i = 0; i < transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject;
        // onlarýn oluþturucusunu deðiþtiriyoruz
        foreach (GameObject go in characterList)
            go.SetActive(false);
        // seçilen karaktere geçiyoruz
        if (characterList[index])
            characterList[index].SetActive(true);

        /*buradan sonraki kodlar seçilen karakteri aktif edip diðerlerini deaktif etmek için yazýyoruz
        */
    }

        public void ToggleLeft()
        {
            characterList[index].SetActive(false);

            index--;//index -= 1; index = index - 1;
            if (index < 0)
                index = characterList.Length - 1;
            // yeni modeli aç
            characterList[index].SetActive(true);
        }
        public void ToggleRight()
        {
            characterList[index].SetActive(false);

            index++;//index -= 1; index = index - 1; 
            if (index == characterList.Length)
                index = 0;
            // yeni modeli aç
            characterList[index].SetActive(true);
        }
        //sahne deðiþtirme ekraný
        public void KarakteriSec()
        {
            PlayerPrefs.SetInt("CharacterSelected", index);
            // SceneManager.LoadScene("Oyun");
        }
    }