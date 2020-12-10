
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public bool cursorVisible = true;
    public bool cursorLock = false;

    Rigidbody playerRigidbody;
    Camera playerCamera;

    public float speed;
    public float speedBoost;
    Vector3 playerVelocity;

    float cameraXRotation;
    public float cameraMaxXAngle = 60;
    public float cameraMinXAngle = -60;

    public bool isDead = false;

    private void Start()
    {
        Cursor.visible = cursorVisible;
        if(cursorLock)
            Cursor.lockState = CursorLockMode.Locked;

        if (speed <= 1)
            speed = 1.5f;
        if (speedBoost <= 1)
            speedBoost = 1.5f;
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        cameraXRotation = playerCamera.transform.localRotation.eulerAngles.x;
    }

    

    void MovingXZ()
    {
        playerVelocity = new Vector3(0,playerRigidbody.velocity.y,0);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= speedBoost;
        if (Input.GetKey(KeyCode.W))
            playerVelocity += transform.forward * speed;
        if (Input.GetKey(KeyCode.S))
            playerVelocity -= transform.forward * speed;
        if (Input.GetKey(KeyCode.D))
            playerVelocity += transform.right * speed;
        if (Input.GetKey(KeyCode.A))
            playerVelocity -= transform.right * speed;
        playerRigidbody.velocity = playerVelocity;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= speedBoost;
        
       // if(playerRigidbody.velocity != Vector3.zero && playerRigidbody.velocity.y == 0)
        //    SoundManager.current.PlaySound(SoundManager.Sound.FootStep, transform.position);
        
    }

    void Rotating()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);       //  Gdy przemieszczasz mysz po osi X to obracasz gracza w lewo/prawo (oś obrotu gracza Y)

        cameraXRotation -= Input.GetAxis("Mouse Y");
        cameraXRotation = Mathf.Clamp(cameraXRotation, cameraMinXAngle, cameraMaxXAngle);   // upewnienie się, że wartość nie przekroczy minimum i maximum
        playerCamera.transform.rotation = Quaternion.Euler(cameraXRotation, playerCamera.transform.rotation.eulerAngles.y, 0.0f);    // przypisanie quaterniona z odpowiednimi parametrami do kamery
    }

    private void Update()
    {
        MovingXZ();
        Rotating();

        if (isDead == true)
        {
            Debug.Log("Umarłeś");
        }
        
    }

}
