using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Level2 : MonoBehaviour
{
    //LEVEL2
    [Header("TEXTO UI")]
    public Text text;
    [Space]
    [Header("VARIABLES")]
    public GameObject Computer;
    public GameObject SafeBox;
    public GameObject Papers;
    public GameObject Usb;
    [Space]
    public Light computerLight;
    [Space]
    public bool computerPassword = false;
    public bool safeboxPassword = false;
    public bool usb = false;
    [Space]
    public GameObject BlindWalkros;
    [Space]
    public GameObject TpLevel2To1;
    [Space]
    public Animator SafeBoxAnim;

    [Header("CAMERAS")]
    //cameras
    public Camera camNormal;
    public Camera camEvent2;

    [Header("CANVAS ELEMENTS")]
    public RawImage caveMapImage;
    public RawImage passwordInPcImage;
    public RawImage passwordInPapersImage;


    void Start()
    {
        TpLevel2To1.SetActive(false);
        computerLight.gameObject.SetActive(false);
        Usb.SetActive(false);

        text.text = "";
    }
    void Update()
    {
        if (GameManager.Instance.Level2Completed == true)
        {
            TpLevel2To1.SetActive(true);
        }
        if (GameManager.Instance.EventWalkros2 == true)
        {
            camNormal.enabled = false;
            camEvent2.enabled = true;
        }
        if (GameManager.Instance.EventWalkros2 == false)
        {
            camNormal.enabled = true;
            camEvent2.enabled = false;
        }
    }

    public void enablePcPassword()
    {
        computerPassword = true;
    }

    IEnumerator disableUIElement()
    {
        yield return new WaitForSeconds(1.5f);
        caveMapImage.gameObject.SetActive(false);
        passwordInPcImage.gameObject.SetActive(false);
        passwordInPapersImage.gameObject.SetActive(false);
    }
    IEnumerator disableMapUI()
    {
        yield return new WaitForSeconds(3f);
        caveMapImage.gameObject.SetActive(false);
    }
}
