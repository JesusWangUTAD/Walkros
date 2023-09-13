using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }


    private void Update()
    {
        if (Input.GetKeyDown("joystick button 1"))
        {
            Application.Quit();
        }
    }
}
