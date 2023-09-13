using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Level3 : MonoBehaviour
{
    //LEVEL3
    [Header("TEXTO UI")]
    public Text text;

    [Header("VARIABLES")]
    public GameObject Stones;
    public GameObject FallenTree3;
    public BoxCollider FallenTree3Coll;

    [Header("CAMERAS")]
    //cameras
    public Camera camNormal;
    public Camera camEvent3;
    public Camera camEvent3Sec;
    void Start()
    {
        text.text = "";
    }
    private void Update()
    {
        if (GameManager.Instance.EventWalkros3 == true)
        {
            camNormal.enabled = false;
            camEvent3.enabled = true;
        }
        if (GameManager.Instance.EventWalkros3 == false)
        {
            camNormal.enabled = true;
            camEvent3.enabled = false;

            if (GameManager.Instance.EventWalkros3Sec == true)
            {
                camNormal.enabled = false;
                camEvent3Sec.enabled = true;
            }
            if (GameManager.Instance.EventWalkros3Sec == false)
            {
                camNormal.enabled = true;
                camEvent3Sec.enabled = false;
            }

        }
    }
}
