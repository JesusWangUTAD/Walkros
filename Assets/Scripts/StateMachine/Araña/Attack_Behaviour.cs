using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack_Behaviour : StateMachineBehaviour
{
    private Transform agentTransform;
    private NavMeshAgent Nagent;
    private GameObject target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Nagent = animator.gameObject.GetComponent<NavMeshAgent>();
        agentTransform = animator.gameObject.GetComponent<Transform>();
        target =GameObject.FindGameObjectWithTag("Player");

        //Speed
        Nagent.speed = 5f;   
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Chase the player
        Nagent.destination = target.transform.position;

        //Distance between Player and Walkros
        float dist = Vector3.Distance(target.transform.position, agentTransform.position);
        if (dist < 1f)
        {
            //Insertar animación de matar al player
            //Evento de animación que ejecuta el void DIE en el script Walkros araña
            Debug.Log("YOU'VE DIED");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
