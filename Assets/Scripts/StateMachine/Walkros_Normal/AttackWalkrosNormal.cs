using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AttackWalkrosNormal : StateMachineBehaviour
{
    private NavMeshAgent AgentWalkrosN;
    private GameObject Player;
    private float distant;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        AgentWalkrosN = animator.gameObject.GetComponent<NavMeshAgent>();
        if (SceneManager.GetActiveScene().name == "1_House")
        {
            GameObject.Find("Main Camera").GetComponent<GM_Level1>().Timer0();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AgentWalkrosN.destination = Player.transform.position;//Va a por el jugador.
        distant = Vector3.Distance(Player.transform.position, AgentWalkrosN.transform.position);//Distancia entre walkros y jugador
        if (distant<2)
        {
            GameManager.Instance.EventWalkros1 = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
