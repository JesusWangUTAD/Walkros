using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Para que se pueda utilizar la IA

public class PatrullaWolf2 : StateMachineBehaviour
{
    private NavMeshAgent Agent; //El NavMeshAgent del Agente
    private int destinationPoint = 0; //Punto de destino
    private Transform[] patrolPointsWolf2; //Waypoints de la patrulla del Wolf2
    private GameObject player; //El jugador
    private GameObject wolf2; //El lobo 2
    private float distance; //La distancia


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player"); // El player es igual al tag "Player"
       
       wolf2 = animator.gameObject; //Cogemos el animator del Wolf2

       Agent = animator.gameObject.GetComponent<NavMeshAgent>(); //Cogemos el NavMeshAgent del Agente

       patrolPointsWolf2 = animator.gameObject.GetComponent<WaypointsWolf2>().PatrolPointsWolf2; //Se cogen los patrolpoints del script WaypointsWolf2

        Agent.destination = patrolPointsWolf2[destinationPoint].position; //El destino del agente es igual al de los waypoints
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f) //Si la distancia es menor que 0.5
        {
            GotoNextPoint(); //Va al siguiente punto

        }

        distance = Vector3.Distance(player.transform.position, wolf2.transform.position); //Distancia entre el wolf2 y el jugador

        if (distance <= 10) //Si es menor que 10 metros
        {
            animator.SetTrigger("Camino"); //Se activa el estado "Camino"
        }
    }

    void GotoNextPoint()
    {
        if (patrolPointsWolf2.Length == 0) //Si no hay waypoints en el array lo de abajo no se ejecuta
        return;
      
        destinationPoint = (destinationPoint + 1) % patrolPointsWolf2.Length; //elige el próximo waypoint en el array y al llegar, vuelve al primero

        Agent.destination = patrolPointsWolf2[destinationPoint].position; //La posición del agente es igual a la posición de los waypoints
    }
}
