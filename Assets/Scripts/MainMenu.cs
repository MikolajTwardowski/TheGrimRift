using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public GameObject ingameMenu;
    public GameObject player;
    
    
    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined; 
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
        
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        ingameMenu.SetActive (false);
        //Cursor.visible = false;
        //Screen.lockCursor = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }
}
