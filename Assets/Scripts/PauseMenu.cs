using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isSaveLoadMenu = false;
    public bool lockCursor = true;
    private bool m_cursorIsLocked = true;

    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject saveMenu;
    public GameObject loadMenu;
    public GameObject invMenu;
    public GameObject craftMenu;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PlayerFPSController.current.enabled = false;
            isPaused = true;
            print("Paused is " + isPaused);
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            PlayerFPSController.current.enabled = true;
            isPaused = false;
            print("Paused is " + isPaused);
            saveMenu.SetActive(false);
            loadMenu.SetActive(false);
            pauseMenu.SetActive(false);

            Inventory.craftingActive = false;
            Inventory.inventoryActive = false;
            invMenu.SetActive(false); //get this to be less redundant
            craftMenu.SetActive(false);
            Time.timeScale = 1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //UpdateCursorLock();
        }
    }

    public void PauseGame()
    {
        PlayerFPSController.current.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        PlayerFPSController.current.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void Resume()
    {
        saveMenu.SetActive(false);
        loadMenu.SetActive(false);
        PlayerFPSController.current.enabled = true;
        isPaused = false;
        pauseMenu.SetActive(false);
        invMenu.SetActive(false); //get this to be less redundant
        craftMenu.SetActive(false);
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




    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}