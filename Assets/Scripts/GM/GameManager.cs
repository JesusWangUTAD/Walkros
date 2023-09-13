using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Header("GAME")]
    public bool isFirstTime = true;

    [Header("PLAYER")]
    [Space]
    public bool stealthOn = false; //Sigilo
    GameObject player;
    public string currentLevel = "";

    [Header("LEVEL0")]
    [Space]
    public bool Jump = true;
    public bool EventWalkros0 = false;
    public bool EventWalkros0Cin = false;

    [Header("LEVEL1")]
    [Space]
    public bool woodenPlanks = false;
    public bool LightFireOn = false;
    public bool LightBoxOn = false;
    public bool roomDoorOpen = false;
    public bool forestDoorOpen = false;

    public bool EventWalkros1 = false;
    public bool EventWalkros1Done = false;
    public bool EventBird = false;

    public bool spawn1point = false;
    public bool spawn2point = false;
    public bool spawn3point = false;
    public bool spawnDone = false;

    [Header("LEVEL2")]
    [Space]
    public bool EventWalkros2 = false;
    public bool Level2Completed = false;

    [Header("LEVEL3")]    
    [Space]
    public bool EventWalkros3 = false;
    public bool EventWalkros3Sec = false;
    public bool stonesL3 = false;

    [Header("LEVEL4")]
    [Space]
    public bool EventWalkros4 = false;
    public bool unlockLabDoor = false;

    private void Awake()
    {
        #region SINGLETON
        //SINGLETON
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        #region SETEO DE VARIABLES DE GUARDADO
        //LEVEL
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            Debug.Log("HAY UN GUARDADO");
            currentLevel = PlayerPrefs.GetString("CurrentLevel");
        }
        else
        {
            //Debug.Log("NO HAY GUARDADO EN PLAYERPREFS");
            PlayerPrefs.SetString("CurrentLevel", "0_Start");
        }
        //FIRST TIME PLAYING
        if (PlayerPrefs.HasKey("TimesPlaying"))
        {
            //Debug.Log("NO ES LA PRIMERA VEZ JUGANDO");
            isFirstTime = ConvertIntToBool(PlayerPrefs.GetInt("TimesPlaying"));
        }
        else
        {
            Debug.Log("PRIMERA VEZ JUGANDO");
            PlayerPrefs.SetInt("TimesPlaying", 1);//true
            isFirstTime = ConvertIntToBool(PlayerPrefs.GetInt("TimesPlaying"));
        }

        #endregion
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "0_Start")
        {
            Jump = false;
        }
    }
    private void Update()
    {
        #region SPAWNPOINT PLAYER LEVEL 1
        //SpawnPoint del jugador al entrar en el nivel 1 (La casa)
        if (SceneManager.GetActiveScene().name == "1_House")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (spawn1point == true && spawnDone == false)
            {
                Debug.Log("1");
                player.transform.position = GameObject.Find("Main Camera").GetComponent<GM_Level1>().spawn1.position;
                spawnDone = true;
            }
            else if (spawn2point == true && spawnDone == false)
            {
                Debug.Log("2");
                player.transform.position = GameObject.Find("Main Camera").GetComponent<GM_Level1>().spawn2.position;
                spawnDone = true;
            }
            else if (spawn3point == true && spawnDone == false)
            {
                Debug.Log("3");
                player.transform.position = GameObject.Find("Main Camera").GetComponent<GM_Level1>().spawn3.position;
                spawnDone = true;
            }
        }
        #endregion
    }

    //LEVEL0
    public void enableJump()
    {
        Jump = true;
        Debug.Log("JumpEnabled");
    }
    //LEVEL1
    public void takeWoodenplanks()
    {
        woodenPlanks = true;
        Debug.Log("TakeWoodenPlanks" + woodenPlanks);
    }
    public void enableFireLight()
    {
        LightFireOn = true;
    }
    public void enableLightboxLight()
    {
        LightBoxOn = true;
    }
    //LEVEL3
    public void throwStones()
    {
        stonesL3 = true;
    }
    //LEVEL4
    public void unlockingLabDoor()
    {
        unlockLabDoor = true;
    }
    
    #region SAVE SYSTEM

    #region SAVE
    public void SaveSystem()
    {
        //Guarda el nivel en el que estabas
        PlayerPrefs.SetString("CurrentLevel", currentLevel);
        //Game Variables
        PlayerPrefs.SetInt("TimesPlaying", ConvertBoolToInt(isFirstTime));
        //Level 0 Variables
        PlayerPrefs.SetInt("JumpState", ConvertBoolToInt(Jump));
        //Level 1 Variables
        PlayerPrefs.SetInt("LightFireState", ConvertBoolToInt(LightFireOn));
        PlayerPrefs.SetInt("LightBoxState", ConvertBoolToInt(LightBoxOn));
        PlayerPrefs.SetInt("RoomDoorState", ConvertBoolToInt(roomDoorOpen));
        PlayerPrefs.SetInt("ForestDoorState", ConvertBoolToInt(forestDoorOpen));
        PlayerPrefs.SetInt("EventWalkros1", ConvertBoolToInt(EventWalkros1Done));
        PlayerPrefs.SetInt("SpawnPoint1", ConvertBoolToInt(spawn1point));
        PlayerPrefs.SetInt("SpawnPoint2", ConvertBoolToInt(spawn2point));
        PlayerPrefs.SetInt("SpawnPoint3", ConvertBoolToInt(spawn3point));
        PlayerPrefs.SetInt("SpawnPoint", ConvertBoolToInt(spawnDone));
        //Level 2 Variables
        PlayerPrefs.SetInt("EventWalkros2", ConvertBoolToInt(EventWalkros2));
        PlayerPrefs.SetInt("Level2State", ConvertBoolToInt(Level2Completed));
        //Level 3 Variables
        PlayerPrefs.SetInt("EventWalkros3", ConvertBoolToInt(EventWalkros3));
        PlayerPrefs.SetInt("EventWalkros3Sec", ConvertBoolToInt(EventWalkros3Sec));
        PlayerPrefs.SetInt("Stones3State", ConvertBoolToInt(stonesL3));
        //Level 4 Variables
        PlayerPrefs.SetInt("EventWalkros4", ConvertBoolToInt(EventWalkros4));
        PlayerPrefs.SetInt("Level4State", ConvertBoolToInt(unlockLabDoor));

        Debug.Log("GAME SAVED");
    }
    #endregion

    #region LOAD
    public void LoadSystem()
    {
        //Carga el nivel en el que estabas
        currentLevel = PlayerPrefs.GetString("CurrentLevel");
        //Game Variables 
        isFirstTime = ConvertIntToBool(PlayerPrefs.GetInt("TimesPlaying"));
        //Level 0 Variables 
        Jump = ConvertIntToBool(PlayerPrefs.GetInt("JumpState"));
        //Level 1 Variables 
        LightFireOn = ConvertIntToBool(PlayerPrefs.GetInt("LightFireState"));
        LightBoxOn = ConvertIntToBool(PlayerPrefs.GetInt("LightBoxState"));
        roomDoorOpen = ConvertIntToBool(PlayerPrefs.GetInt("RoomDoorState"));
        forestDoorOpen = ConvertIntToBool(PlayerPrefs.GetInt("ForestDoorState"));
        EventWalkros1Done = ConvertIntToBool(PlayerPrefs.GetInt("EventWalkros1"));
        spawn1point = ConvertIntToBool(PlayerPrefs.GetInt("SpawnPoint1"));
        spawn2point = ConvertIntToBool(PlayerPrefs.GetInt("SpawnPoint2"));
        spawn3point = ConvertIntToBool(PlayerPrefs.GetInt("SpawnPoint3"));
        spawnDone = ConvertIntToBool(PlayerPrefs.GetInt("SpawnPoint"));
        //Level 2 Variables 
        EventWalkros2 = ConvertIntToBool(PlayerPrefs.GetInt("EventWalkros2"));
        Level2Completed = ConvertIntToBool(PlayerPrefs.GetInt("Level2State"));
        //Level 3 Variables 
        EventWalkros3 = ConvertIntToBool(PlayerPrefs.GetInt("EventWalkros3"));
        EventWalkros3Sec = ConvertIntToBool(PlayerPrefs.GetInt("EventWalkros3Sec"));
        stonesL3 = ConvertIntToBool(PlayerPrefs.GetInt("Stones3State"));
        //Level 4 Variables 
        EventWalkros4 = ConvertIntToBool(PlayerPrefs.GetInt("EventWalkros4"));
        unlockLabDoor = ConvertIntToBool(PlayerPrefs.GetInt("Level4State"));

        Debug.Log("GAME LOADED");
    }
    #endregion

    #region CONVERTERS
    public int ConvertBoolToInt(bool boolean)
    {
        int intRestult;
        if(boolean == true)
        {
            intRestult = 1;
            return intRestult;
        }
        else
        {
            intRestult = 0;
            return intRestult;
        }
    }
    public bool ConvertIntToBool(int integer)
    {
        bool boolResult;
        if(integer == 1)
        {
            boolResult = true;
            return boolResult;
        }
        else
        {
            boolResult = false;
            return boolResult;
        }
    }
    #endregion

    #endregion

}
