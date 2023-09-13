using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolWalkrosNormal : StateMachineBehaviour
{
    private NavMeshAgent AgentWalkrosN;
    private int destPoint = 0;
    private Transform[] patrolPoints;
    private Vector3 Walkros_Player; //Entre jugador y walkros, línea que une
    private float angl;//Angulo

    private float distant;
    private GameObject Player;
    private GameObject WalkrosN;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        WalkrosN = animator.gameObject;
        AgentWalkrosN = animator.gameObject.GetComponent<NavMeshAgent>();
        patrolPoints = animator.gameObject.GetComponent<WalkrosAraña>().PatrolPoints;//Coge los puntos del otro script y rellena con ellos la array de transform
        AgentWalkrosN.destination = patrolPoints[destPoint].position;//Le digo que vaya a un destino (los puntos)
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!AgentWalkrosN.pathPending && AgentWalkrosN.remainingDistance < 1f)
        {
            GotoNextPoint();
        }

        angl = Vector3.Angle(Walkros_Player, Vector3.forward);//Angulo entre el Walkros mirando hacia delante (forward) y el player(Linea entre ambos)
        if (angl <= 15)//Rango de visión.15º  hacia ambos lados, 30º total.
        {
            distant = Vector3.Distance(Player.transform.position, WalkrosN.transform.position);//Distancia entre walkros y jugador
            if (distant <= 15)//Si es menos o igual a 15 (Rango)
            {
                RaycastHit raycast;
                if(Physics.Raycast(animator.gameObject.transform.position,animator.gameObject.transform.forward,out raycast, 15))
                {
                    if(raycast.transform.gameObject.tag == "Player")
                    {
                        animator.SetBool("Detect", true);//Pasa al estado Alert
                    }
                }               
            }            
        }
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
            return;
        destPoint = (destPoint + 1) % patrolPoints.Length;// Cada vez que llegue a un punto, que vaya al siguiente,y cuando llegue al final, se repita la rutina, el porcentaje etc es para que no se salga del array, asi que si llega al final, va al primero.
        // Set the agent to go to the currently selected destination.
        AgentWalkrosN.destination = patrolPoints[destPoint].position;
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
