using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkrosNormalEvento : StateMachineBehaviour
{
    private NavMeshAgent AgentWalkrosN;
    private GameObject Player;
    private Transform agentTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentTransform = animator.gameObject.GetComponent<Transform>();
        AgentWalkrosN = animator.gameObject.GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.forward * 15f, Color.green);
        Vector3 vect = Player.transform.position - agentTransform.position; //Vector beween Walkros and Player
        float angle = Vector3.Angle(animator.gameObject.transform.forward, vect);//Angle between Walkros forward and Player 
        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), vect * 15f, Color.yellow);
        if (angle <= 180) //90 DEGREES
        {
            float distance = Vector3.Distance(Player.transform.position, agentTransform.position); //Distance between Walkros and Player
            if (distance <= 10) // 15 METERS
            {
                RaycastHit raycast; //Raycast
                if (Physics.Raycast(animator.gameObject.transform.position,vect, out raycast, 10))
                {
                    if (raycast.transform.gameObject.tag == "Player") //Hit
                    {
                        Debug.Log("DSDSDDSD");
                        animator.SetTrigger("attack"); //Attack state
                    }
                    else //Not Hit
                    {
                        Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.forward * 15f, Color.blue);
                    }
                }
            }
        }
        if (GameManager.Instance.stonesL3 == true) //Walkros level3 SEGUNDO EVENTO (piedras)
        {
            AgentWalkrosN.destination = Player.transform.position; //Va al Scape point.
            animator.SetTrigger("EventWalkrosL3");
        }
    }

}
