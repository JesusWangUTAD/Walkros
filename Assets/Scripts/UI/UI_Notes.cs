using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Notes : MonoBehaviour
{
    public Texture2D[] notes = new Texture2D[10];
    public RawImage noteHandler;

    void Start()
    {
        noteHandler.gameObject.SetActive(false);
    }

    public void showNote(int noteSprite)
    {
        noteHandler.texture = notes[noteSprite];
        noteHandler.gameObject.SetActive(true);
    }

    public void hideNote()
    {
        noteHandler.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("joystick button 1"))
        {
            noteHandler.gameObject.SetActive(false);
        }
    }
}
