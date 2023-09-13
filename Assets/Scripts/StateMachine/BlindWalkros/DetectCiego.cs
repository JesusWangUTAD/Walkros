using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectCiego : StateMachineBehaviour
{
    private NavMeshAgent nAgent;

    private Transform agentTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.position = animator.gameObject.GetComponent<WalkrosAraña>().StartWalkros.position;
        nAgent = animator.gameObject.GetComponent<NavMeshAgent>();

        agentTransform = animator.gameObject.GetComponent<Transform>();

        //Speed
        nAgent.speed = 3f;

        nAgent.destination = GameObject.Find("WindowsBlind").transform.position;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Raycast
        RaycastHit Rhit;
        if (Physics.Raycast((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.TransformDirection(Vector3.forward), out Rhit, 5f)) // 5 METROS
        {
            if (Rhit.collider.tag == "Player") //Hit
            {
                Debug.Log("attack");
                animator.SetBool("Attack", true); //Attack state
                animator.SetBool("Detect", false);
            }
        }

        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.transform.forward * 5f, Color.blue);
        if (!nAgent.pathPending && nAgent.remainingDistance < 0.5f)
        {

            animator.SetBool("Pivot", true);
            animator.SetBool("Detect", false);

        }

    }


}
