using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hareket : MonoBehaviour
{

    //public GameObject Oyuncu;

    void Update()
    {
    if (Input.GetButtonDown("Otur"))
    {
    GetComponent<Animator>().Play("sitting");
    }
    }
    }