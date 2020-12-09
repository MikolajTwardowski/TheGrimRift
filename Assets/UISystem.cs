using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    
    
    Camera playerCamera;
    public GameObject OreMessage;
    public GameObject TakeMessage;
    
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }
    
    void Update()
    {
        CheckAndShowMessage();
    }
    
    
    void CheckAndShowMessage()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out hit, 2))
        {

            if ((hit.collider.CompareTag("Ore")))
            {
                OreMessage.SetActive(true);
                TakeMessage.SetActive(false);
            }
            
            else if ((hit.collider.CompareTag("Pickaxe")) || (hit.collider.CompareTag("BlueShard")) || (hit.collider.CompareTag("YellowShard")) || (hit.collider.CompareTag("PurpleShard")))
            {
                TakeMessage.SetActive(true);
                OreMessage.SetActive(false);
            }
            else
            {
                TakeMessage.SetActive(false);
                OreMessage.SetActive(false); 
            }

        }
    }
}
