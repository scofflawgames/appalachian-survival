using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isSaveLoadMenu = false;

    public GameObject pauseMenu;
    public GameObject saveMenu;
    public GameObject loadMenu;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            //PlayerFPSController.current.enabled = false;
            isPaused = true;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        //else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        //{
            //PlayerFPSController.current.enabled = true;
          //  isPaused = false;
            //saveMenu.SetActive(false);
            //loadMenu.SetActive(false);
            //pauseMenu.SetActive(false);
            //Time.timeScale = 1;
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
       // }
    }

    public void PauseGame()
    {
        //PlayerFPSController.current.enabled = false;
        //isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        //PlayerFPSController.current.enabled = true;
        //isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void Resume()
    {
        //PlayerFPSController.current.enabled = true;
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        //SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        if (Application.isEditor)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Application.Quit();
        }
    }
}