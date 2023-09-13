using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol_Behaviour : StateMachineBehaviour
{
    private Transform[] patrolPoints;

    private bool bone;
    private GameObject target;

    private NavMeshAgent NAgent;
    private Transform agentTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentTransform = animator.gameObject.GetComponent<Transform>();
        NAgent = animator.gameObject.GetComponent<NavMeshAgent>();

        //Target (player)
        target = animator.gameObject.GetComponent<WalkrosAraña>().Target;

        //Speed
        NAgent.speed = 3f;

        //If WalkrosAraña is using this script
        patrolPoints = animator.gameObject.GetComponent<WalkrosAraña>().PatrolPoints;

        //Patrol first point
        NAgent.destination = patrolPoints[0].transform.position; 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Bone
        bone = animator.gameObject.GetComponent<WalkrosAraña>().bone;
        if (bone == true)
        {
            Debug.Log("alert");
            animator.SetBool("patrol", false);
            animator.SetTrigger("alert"); //Alert state
        }

        //Patrol
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float dist = Vector3.Distance(patrolPoints[i].transform.position, agentTransform.position);

            if (dist < 2f)
            {
                if (i < patrolPoints.Length - 1)
                {
                    NAgent.destination = patrolPoints[i + 1].transform.position;
                }
                else if (i == patrolPoints.Length - 1)
                {
                    Array.Reverse(patrolPoints);
                    i = 0;
                }
            }
        }

        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.forward * 15f, Color.green);
        Vector3 vect = target.transform.position - agentTransform.position; //Vector beween Walkros and Player
        float angle = Vector3.Angle(animator.gameObject.transform.forward, vect);//Angle between Walkros forward and Player 
        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), vect * 15f, Color.yellow);
        if (angle <= 45) //90 DEGREES
        {
            float distance = Vector3.Distance(target.transform.position, agentTransform.position); //Distance between Walkros and Player
            if (distance <= 15) // 15 METERS
            {
                RaycastHit raycast; //Raycast
                if (Physics.Raycast(animator.gameObject.transform.position, animator.gameObject.transform.forward, out raycast, 15))
                {
                    if (raycast.transform.gameObject.tag == "Player") //Hit
                    {
                        animator.SetTrigger("attack"); //Attack state
                    }
                    else //Not Hit
                    {
                        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.forward * 15f, Color.blue);
                    }
                }
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("patrol", false);
        animator.gameObject.GetComponent<WalkrosAraña>().bone = false;
    }

}
