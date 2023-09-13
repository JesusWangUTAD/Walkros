using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class WalkrosNormal01_Idle : StateMachineBehaviour
{

    private NavMeshAgent AgentWalkrosN;
    private GameObject Player;
    float distant;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AgentWalkrosN = animator.gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        if (SceneManager.GetActiveScene().name == "1_House")
        {
            GameObject.Find("Main Camera").GetComponent<GM_Level1>().Timer0();
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distant = Vector3.Distance(Player.transform.position, AgentWalkrosN.transform.position);//Distancia entre lobo y jugador
        if (SceneManager.GetActiveScene().name == "0_Start")
        {
            if (distant <= 10) //Walkros level0
            {
                animator.SetBool("EventWalkrosNormal", true);
            }

        }
        if (SceneManager.GetActiveScene().name == "1_House")
        {
            if (GameManager.Instance.EventWalkros1 == false) //Walkros level1
            {
                animator.SetBool("EventWalkrosL1", false);
            }
            if (GameManager.Instance.EventWalkros1 == true) //Walkros level1
            {
                animator.SetBool("EventWalkrosL1", true);
            }

        }


    }

}
