
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public bool cursorVisible = true;
    public bool cursorLock = false;

    public bool blockActions = false;

    Rigidbody playerRigidbody;
    Camera playerCamera;

    public float speed;
    public float speedBoost;
    Vector3 playerVelocity;

    public bool canMine = false;

    float cameraXRotation;
    public float cameraMaxXAngle = 60;
    public float cameraMinXAngle = -60;
    [SerializeField] float time = 10;
    
    [SerializeField] GameObject dethCanvas;
    
        private bool _isDead = false;
        public bool isDead 
        {
            get { return _isDead; }
            set
            {
                if (_isDead != value)
                    _isDead = value;
                dethCanvas.SetActive(true);
            }
        }

    //public bool isDead = false;

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
        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= speedBoost;
            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Animator>().SetBool("isRunning", true);
                playerVelocity += transform.forward * speed;
            }
        }*/
        
        if (Input.GetKey(KeyCode.W))
        {
            
            

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GetComponent<Animator>().SetBool("isRunning", true);
                speed *= speedBoost;
                playerVelocity += transform.forward * speed;
            }
            else
            {
                GetComponent<Animator>().SetBool("isWalking", true);
                playerVelocity += transform.forward * speed;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                playerVelocity += transform.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerVelocity -= transform.right;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Animator>().SetBool("isRunning", false);
            speed = 1;
            GetComponent<Animator>().SetBool("isBacking", true);
            playerVelocity += (transform.forward * -1) * speed;
            
            if (Input.GetKey(KeyCode.D))
            {
                playerVelocity += transform.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerVelocity -= transform.right;
            }
        }
        /*else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("isRunning", false);
            speed = 1;
            playerVelocity += transform.right * speed;
            GetComponent<Animator>().SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("isRunning", false);
            speed = 1;
            playerVelocity -= transform.right * speed;
            GetComponent<Animator>().SetBool("isWalking", true);
        }*/
        

        else
        {
            GetComponent<Animator>().SetBool("isWalking",false);
            GetComponent<Animator>().SetBool("isBacking",false);
            GetComponent<Animator>().SetBool("isRunning", false);
            GetComponent<Animator>().SetBool("isMining", false);
            GetComponent<Animator>().SetBool("isPicking", false);
        }
        
        playerRigidbody.velocity = playerVelocity;
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //speed /= speedBoost;
            speed = 1;
            GetComponent<Animator>().SetBool("isRunning", false);
        }
        
        if (Input.GetKey(KeyCode.M))
            canMine = true;
        
        
        if (Input.GetKey(KeyCode.P))
        {
            GetComponent<Animator>().SetBool("isPicking", true);
            playerVelocity += transform.forward * speed;
        }
        
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

    public void FootstepSound()
    {
        SoundManager.current.PlaySound(SoundManager.Sound.Footstep);
    }
    
    public void MiningSound()
    {
        SoundManager.current.PlaySound(SoundManager.Sound.Mining);
    }
    
    private void Update()
    {
        if (!blockActions)
        {
            MovingXZ();
            Rotating();
        }

        if(canMine)
            Mining();

        if (isDead == true)
        {
            Debug.Log("Umarłeś");
            //GetComponent<MyPlayerController>().enabled = false;
            blockActions = true;
            GetComponent<Animator>().SetBool("isDead", true);
        }
        
    }
    
    void Mining()
    {
        blockActions = true;
        GetComponent<Animator>().SetBool("isWalking",false);
        GetComponent<Animator>().SetBool("isBacking",false);
        GetComponent<Animator>().SetBool("isRunning", false);
        GetComponent<Animator>().SetBool("isPicking", false);
        GetComponent<Animator>().SetBool("isMining", true);
        playerVelocity += transform.forward * speed;

        time -= Time.deltaTime;
        Debug.Log(time);
        if(time < 0)
        {
            canMine = false;
            GetComponent<Animator>().SetBool("isMining", false);
            time = 10;
            blockActions = false;
        }
    }

}
