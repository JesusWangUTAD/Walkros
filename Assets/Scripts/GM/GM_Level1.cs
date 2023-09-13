using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GM_Level1 : MonoBehaviour
{
    //LEVEL1
    [Header("TEXTO UI")]
    public Text text;

    [Header("INTERACTUABLES")]
    public GameObject woodenPlanks;
    public GameObject LightBox;
    public GameObject fireplaceThings;
    public GameObject Cage;
    [Space]
    public bool birdCageOpen = false; //Se abre la jaula del pájaro. Empieza cerrada (= false)

    [Header("TPS")]
    public GameObject TpLevel1To2;
    public GameObject TpLevel1To3;

    [Header("LIGHTS and PARTICLES")]
    public Light BoxPointLight1;
    public Light BoxPointLight2;
    public Light FirePointLight;
    public GameObject FireParticleSystem;

    [Header("SPAWN POINTS")]
    //SpawnPoints
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;

    [Header("CAMERAS")]
    //cameras
    public Camera camNormal;
    public Camera camEvent1;
    public Camera camBird;

    [Header("EVENTS")]
    public bool birdSoundOn = false;
    public UnityEvent BirdEvent;

    public float timer;

    private void Awake()
    {
        Debug.LogWarning("TIMER 0 " + timer);
        timer = 0;
    }
    void Start()
    {
        if (GameObject.Find("Josua3").GetComponent<Player>().inLevel1 == true)
        {
            Debug.LogError("TIMER 0 " + timer);
            timer = 0;
        }
        //FirePointLight.gameObject.SetActive(false);
        //FireParticleSystem.SetActive(false);
        //BoxPointLight1.gameObject.SetActive(false);
        //BoxPointLight2.gameObject.SetActive(false);
        //TpLevel1To2.SetActive(false);
        //TpLevel1To3.SetActive(false);

        if (GameManager.Instance.LightFireOn == true)
        {
            FirePointLight.gameObject.SetActive(true);
            FireParticleSystem.SetActive(true);
        }
        if (GameManager.Instance.LightBoxOn == true)
        {
            BoxPointLight1.gameObject.SetActive(true);
            BoxPointLight2.gameObject.SetActive(true);
        }
        if (GameManager.Instance.roomDoorOpen == true)
        {
            TpLevel1To2.SetActive(true);
        }
        if (GameManager.Instance.forestDoorOpen == true)
        {
            TpLevel1To3.SetActive(true);
        }
        if (GameManager.Instance.EventWalkros1Done == true)
        {
            woodenPlanks.SetActive(false);
        }

        text.text = "";
    }

    void Update()
    {
        if (GameManager.Instance.LightFireOn == true)
        {
            FirePointLight.gameObject.SetActive(true);
            FireParticleSystem.SetActive(true);
        }
        if (GameManager.Instance.LightBoxOn == true)
        {
            BoxPointLight1.gameObject.SetActive(true);
            BoxPointLight2.gameObject.SetActive(true);
        }
        if (GameManager.Instance.roomDoorOpen == true)
        {
            TpLevel1To2.SetActive(true);
        }
        if(GameManager.Instance.forestDoorOpen == true)
        {
            TpLevel1To3.SetActive(true);
        }
        if (GameManager.Instance.EventWalkros1 == true)
        {
            camNormal.enabled = false;
            camEvent1.enabled = true;
        }
        if (GameManager.Instance.EventWalkros1 == false)
        {
            camNormal.enabled = true;
            camEvent1.enabled = false;
        }
        if (GameManager.Instance.EventBird == true)
        {
            camNormal.enabled = false;
            camBird.enabled = true;
        }
        if (GameManager.Instance.EventBird == false)
        {
            camNormal.enabled = true;
            camBird.enabled = false;
        }

        //Bird event
        if (birdSoundOn == true)
        {
            BirdEvent.Invoke();
        }
    }

    public void Timer0()
    {
        timer = 0;
    }
    public void event1Done()
    {
        GameManager.Instance.EventWalkros1Done = true;
    }
}
