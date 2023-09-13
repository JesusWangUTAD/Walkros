using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Camino : StateMachineBehaviour
{
    private NavMeshAgent Agent; //El NavMeshAgent del Agente
    private GameObject Target; //Target


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent = animator.GetComponent<NavMeshAgent>(); //se coge el componente NavMeshAgent

        Target = animator.gameObject.GetComponent<WaypointsWolf2>().Target; //Se coge el target del script WaypointsWolf2

        Agent.destination = Target.transform.position; //El destino del agente es igual al del target
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
