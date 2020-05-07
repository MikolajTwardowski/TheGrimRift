using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
       // if (other.gameObject.CompareTag("Player"))
        //{
            Debug.Log("Koniec gry!");
            Application.Quit();
        //}
    }

}
