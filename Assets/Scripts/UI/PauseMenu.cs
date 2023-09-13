using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool pausedGame = false;

    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public static PauseMenu pausemenu; //referencia al script

    public GameObject mainCamera;

    public SoundClip saveSound;

    private void Awake()
    {
        //comprobación para que solo haya 1 en la escena
        if(pausemenu == null)
        {
            pausemenu = this;
        }
        else if (pausemenu != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetButtonDown("Pause")) //Tecla escape para pausar el juego
        {
            if (pausedGame == true)
            {
                Resume();
            }
            else if(pausedGame == false)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pausedGame = false;

        //Quita la pausa del juego
        Time.timeScale = 1f;
        //Quita la pausa de la cámara
        mainCamera.GetComponent<ThirdPersonOrbitCamBasic>().horizontalAimingSpeed = 4;
        mainCamera.GetComponent<ThirdPersonOrbitCamBasic>().verticalAimingSpeed = 4;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        pausedGame = true;

        //Pausa el juego
        Time.timeScale = 0f; 
        //Pausa la cámara
        mainCamera.GetComponent<ThirdPersonOrbitCamBasic>().horizontalAimingSpeed = 0;
        mainCamera.GetComponent<ThirdPersonOrbitCamBasic>().verticalAimingSpeed = 0;
    }

    public void Exit()
    {
        Debug.Log("QuitGame"); //Solo funciona en la build, no en el editor
        Application.Quit();
    }

    public void Save()
    {
        Debug.Log("SavingGame...");
        saveSound.PlayClip(this.gameObject);
        GameManager.Instance.SaveSystem();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }
}
