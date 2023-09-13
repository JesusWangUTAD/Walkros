using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sleep_Behaviour : StateMachineBehaviour
{
    private Transform agentTransform; //Agent's transform
    private GameObject target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentTransform = animator.gameObject.GetComponent<Transform>();
        target = animator.gameObject.GetComponent<WalkrosAraña>().Target;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Distance between Walkros and Player
        float dist = Vector3.Distance(target.transform.position, agentTransform.position);
        if (dist < 20f )
        {
            Debug.Log("awake");
            animator.SetTrigger("awake");
        }
        //Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.transform.forward * 20f, Color.red);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("patrol", false);
    }
}
