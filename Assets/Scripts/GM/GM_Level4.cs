using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Level4 : MonoBehaviour
{
    //LEVEL4
    [Header("TEXTO UI")]
    public Text text;

    [Header("VARIABLES")]
    public GameObject LabKey;
    public GameObject Bone;
    public GameObject LabDoor;

    public GameObject TpLevel4To5;

    [Header("CAMERAS")]
    //cameras
    public Camera camNormal;
    public Camera camEvent4;

    void Start()
    {
        text.text = "";
        TpLevel4To5.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.EventWalkros4 == true)
        {
            camNormal.enabled = false;
            camEvent4.enabled = true;
        }
        if (GameManager.Instance.EventWalkros4 == false)
        {
            camNormal.enabled = true;
            camEvent4.enabled = false;
        }
    }
}
