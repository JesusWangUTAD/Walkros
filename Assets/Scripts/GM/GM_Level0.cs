using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Level0 : MonoBehaviour
{
    //LEVEL0
    [Header("VARIABLES")]
    public GameObject FirstAidKit;
    public GameObject FallenTree1;
    public BoxCollider FallenTree1Coll;
    public GameObject FallenTree2;
    public BoxCollider FallenTree2Coll;
    public Text text;

    //cameras
    [Header("CAMERAS")]
    public Camera camNormal;
    public Camera camEvent0;

    void Start()
    {
        text.text = "";
    }

    private void Update()
    {
        if (GameManager.Instance.EventWalkros0Cin == true)
        {
            camNormal.enabled = false;
            camEvent0.enabled = true;
        }
        if (GameManager.Instance.EventWalkros0Cin == false)
        {
            camNormal.enabled = true;
            camEvent0.enabled = false;
        }
    }
}
