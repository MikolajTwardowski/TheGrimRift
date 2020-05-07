using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float speed = 4;
    float gravity = 1;
    public float jumpspeed = 30;
    bool isHoldingItem = false;
    public GameObject hand;
    Vector3 moveDirection;
    public GameObject pickaxePrefab;
    Vector3 throwing;
    public GameObject crystalPrefab;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();

        Target();
        
        if (Input.GetKeyDown(KeyCode.F) && (isHoldingItem == true))
            Throw();
    }

    
    void Target()
    {
        PlayerCamera cam = Camera.main.GetComponent<PlayerCamera>();
        
           if (Physics.Raycast(cam.transform.position, cam.transform.forward, out cam.hit))
           {
               //Debug.Log(cam.hit.transform.name);
               if (cam.hit.transform.CompareTag("Item"))
               {
                   if (Input.GetKeyDown(KeyCode.E))
                   {
                       if (isHoldingItem == true) 
                           Throw();
                       
                       if(cam.hit.transform.GetChild(0).name == "pickaxe")
                           hand.transform.GetChild(0).gameObject.SetActive(true);
                       else if (cam.hit.transform.GetChild(0).name == "Crystal")
                           hand.transform.GetChild(1).gameObject.SetActive(true);
                       
                       Destroy(cam.hit.collider.gameObject);
                       isHoldingItem = true;
                   }
               }
               
               else if (cam.hit.transform.CompareTag("Deposit"))
               {
                   if (Input.GetKeyDown(KeyCode.E) && hand.transform.GetChild(0).gameObject.activeSelf == true)
                   {
                       cam.hit.transform.GetComponent<Deposit>().dropCrystal = true;
                   }
                   else if (Input.GetKeyDown(KeyCode.E) && hand.transform.GetChild(0).gameObject.activeSelf == false)
                   {
                       Debug.Log("Brak kilofa");
                   }
               }
           }
    }
    
    void Throw()
    {
        throwing = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            
        if (hand.transform.GetChild(0).gameObject.activeSelf == true)
        {
            hand.transform.GetChild(0).gameObject.SetActive(false);
            GameObject throwedPickaxe;
            throwedPickaxe = Instantiate(pickaxePrefab, transform.position, transform.rotation);
            throwedPickaxe.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
        } 
        else if (hand.transform.GetChild(1).gameObject.activeSelf == true)
        {
            hand.transform.GetChild(1).gameObject.SetActive(false);
            GameObject Crystal;
            Crystal = Instantiate(crystalPrefab, transform.position, transform.rotation);
            Crystal.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
        }
        isHoldingItem = false;
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(moveX, 0, moveZ);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y += jumpspeed;
            }
        }
        
        
        moveDirection.y -= gravity;
        controller.Move(moveDirection * Time.deltaTime);

    }
    

}
