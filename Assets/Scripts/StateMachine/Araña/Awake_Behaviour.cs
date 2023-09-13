using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Awake_Behaviour : StateMachineBehaviour
{
    private Transform agentTransform;
    private NavMeshAgent Nagent;

    RaycastHit Rhit;
    float rotation;

    float timer;
    bool timerOn;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Nagent = animator.gameObject.GetComponent<NavMeshAgent>();
        agentTransform = animator.gameObject.GetComponent<Transform>();

        //Speed
        Nagent.speed = 0f;

        //Timer
        timer = 0;
        timerOn = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timerOn == true)
        {
            //Timer
            timer += Time.deltaTime;

            //Agent rotation
            rotation += 30 * Time.deltaTime;
            agentTransform.rotation = Quaternion.Euler(0, rotation, 0); //Pivots on Y axe
        }

        //Raycast
        if (Physics.Raycast((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.TransformDirection(Vector3.forward), out Rhit, 15f)) //Did Hit
        {
            if (Rhit.collider.tag == "Player")
            {
                timerOn = false;
                Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.transform.forward * 15f, Color.red);
                Debug.Log("attack");
                animator.SetTrigger("attack"); //Attack state
            }
        }
        else //Did not Hit
        {
            Debug.DrawRay((new Vector3(agentTransform.position.x, agentTransform.position.y + 1f, agentTransform.position.z)), agentTransform.transform.forward * 15f, Color.blue);
            if (timer >= 3) //If at 3 seconds it has not detected anyone
            {
                timerOn = false;
                Debug.Log("patrol");
                animator.SetBool("patrol", true); //Patrol state
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
    }

}
