 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolCiego : StateMachineBehaviour
{

    private NavMeshAgent nAgent;
    private int destPoint = 0;
    private Transform[] patrolPoints;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nAgent = animator.gameObject.GetComponent<NavMeshAgent>();

        patrolPoints = animator.gameObject.GetComponent<WalkrosAraña>().PatrolPoints;

        nAgent.destination = patrolPoints[destPoint].position;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!nAgent.pathPending && nAgent.remainingDistance < 0.5f)
        {
            GotoNextPoint();

        }
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
            return;
        // Set the agent to go to the currently selected destination.
        nAgent.destination = patrolPoints[destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }
   
}
