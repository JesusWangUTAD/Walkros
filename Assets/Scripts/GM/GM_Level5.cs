using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Level5 : MonoBehaviour
{
    public GameObject WalkrosCapsule;
    public bool capsuleInteracted = false;

    public GameObject PaperDiagnostico;
    public bool paperTaken = false;

    [Header("EVENTS")]
    public GameObject End;
    public GameObject NotEndYet;

    void Update()
    {
        if (capsuleInteracted == true && paperTaken == true)
        {
            NotEndYet.SetActive(false);
            End.SetActive(true);
        }
    }

    public void TakePaperDiagnostico()
    {
        paperTaken = true;
    }
}
