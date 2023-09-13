using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alert_Behaviour : StateMachineBehaviour
{
    private NavMeshAgent NAgent;
    private Transform agentTransform;
    private Transform boneDestination;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        agentTransform = animator.gameObject.GetComponent<Transform>();
        boneDestination = animator.gameObject.GetComponent<WalkrosAraña>().BoneDestination;

        //Speed
        NAgent.speed = 4f;

        //Go to bone destination
        NAgent.destination = boneDestination.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Distance between Walkros and Bone
        float dist = Vector3.Distance(boneDestination.position, agentTransform.position);
        if (dist < 1f)
        {
            Debug.Log("check");
            animator.SetTrigger("check");
        }

        //Raycast
        RaycastHit Rhit;
        if (Physics.Raycast((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.TransformDirection(Vector3.forward), out Rhit, 1f)) // 1 METROS
        {
            if (Rhit.collider.tag == "Player") //Hit
            {
                Debug.Log("attack");
                animator.SetTrigger("attack"); //Attack state
            }
        }
        else //Not Hit
        {
            Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.transform.forward * 5f, Color.blue);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("patrol", false);
    }
}
