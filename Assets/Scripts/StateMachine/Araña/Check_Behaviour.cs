using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Check_Behaviour : StateMachineBehaviour
{
    private NavMeshAgent NAgent;
    private Transform agentTransform;
    private GameObject target;

    float timer = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        agentTransform = animator.gameObject.GetComponent<Transform>();
        target = animator.gameObject.GetComponent<WalkrosAraña>().Target;

        //Speed
        NAgent.speed = 0f;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        //Distance between Walkros and Player
        float dist = Vector3.Distance(target.transform.position, agentTransform.position);
        if (dist < 5f)
        {
            if (GameManager.Instance.stealthOn == false)
            {
                Debug.Log("attack");
                animator.SetTrigger("attack"); //Attack state
            }
        }

        if (timer >= 10) //During 10 seconds player can go to the key
        {
            Debug.Log("patrol");
            animator.SetBool("patrol", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
    }
}
