using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AttackWolfOne : StateMachineBehaviour
{
    private NavMeshAgent AgentWolf1;
    private GameObject Player;
    private GameObject Wolf1;
    private float distant;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Wolf1 = animator.gameObject;
        AgentWolf1 = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AgentWolf1.destination = Player.transform.position;//Va a por el jugador.
        
        distant = Vector3.Distance(Player.transform.position, Wolf1.transform.position);//Distancia entre lobo y jugador

        if (distant <= 2)//Si es menos o igual a 3 (Rango)//El cuerpo mide 
        {
            animator.SetTrigger("Attack");
            // SceneManager.LoadScene("6_Death");//Vamos a la escena de MUERTE.
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
