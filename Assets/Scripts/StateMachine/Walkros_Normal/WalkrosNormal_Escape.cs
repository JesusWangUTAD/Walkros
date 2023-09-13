using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkrosNormal_Escape : StateMachineBehaviour
{
    private NavMeshAgent AgentWalkrosN;
    private GameObject EscapePointL3;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AgentWalkrosN = animator.gameObject.GetComponent<NavMeshAgent>();
        EscapePointL3 = GameObject.FindGameObjectWithTag("EscapePointL3");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AgentWalkrosN.destination = EscapePointL3.transform.position;//Va a por el jugador.
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
