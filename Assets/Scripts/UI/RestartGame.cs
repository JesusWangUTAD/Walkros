using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        //controlsPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown("joystick button 1"))
        {
            controlsPanel.SetActive(false);
            creditsPanel.SetActive(false);
        }
    }
    public void ReStartGame()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void LoadGameSaved()
    {
        if(GameManager.Instance.isFirstTime == false)
        {
            GameManager.Instance.LoadSystem();
            SceneManager.LoadScene(GameManager.Instance.currentLevel);
        }
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.isFirstTime = false;
        SceneManager.LoadScene("InitialKinematic");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("0_Start");
    }

    //Controles
    public void showControls() 
    {
        controlsPanel.SetActive(true);
    }
    public void hideControls()
    {
        controlsPanel.SetActive(false);
    }

    //Créditos
    public void showCredits()
    {
        creditsPanel.SetActive(true);
    }
    public void hideCredits()
    {
        creditsPanel.SetActive(false);
    }
}
