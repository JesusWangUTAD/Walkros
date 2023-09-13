using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpEventScript : MonoBehaviour
{
    public void JumpEvent()
    {
        GameObject.Find("Josua").GetComponent<Player>().velocity = 1;

        if (SceneManager.GetActiveScene().name == "0_Start")
        {
            GameObject.Find("Main Camera").GetComponent<GM_Level0>().FallenTree1Coll.isTrigger = false;
            GameObject.Find("Main Camera").GetComponent<GM_Level0>().FallenTree2Coll.isTrigger = false;
        }

        if (SceneManager.GetActiveScene().name == "3_Forest")
        {
            GameObject.Find("Main Camera").GetComponent<GM_Level3>().FallenTree3Coll.isTrigger = false;
        }
    }
}
