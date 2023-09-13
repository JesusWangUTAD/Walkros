using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IddleCockroach : StateMachineBehaviour
{
    private NavMeshAgent AgentCockRoach;
    private int destPoint = 0;
    private Transform[] patrolPoints;

    private float distant;
    private GameObject Player;
    private GameObject Cockroach;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Cockroach = animator.gameObject;
        AgentCockRoach = animator.gameObject.GetComponent<NavMeshAgent>();
        patrolPoints = animator.gameObject.GetComponent<CockroachDirector>().PatrolPoints;//Coge los puntos del otro script y rellena con ellos la array de transform
        AgentCockRoach.destination = patrolPoints[destPoint].position;//Le ordeno ir a un punto del array
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!AgentCockRoach.pathPending && AgentCockRoach.remainingDistance < 0.1f)// Distancia entre la cucaracha y el punto de ruta que le toca
        {
            GotoNextPoint();// Ir al siguiente punto
        }
        distant = Vector3.Distance(Player.transform.position, Cockroach.transform.position); //Espacio entre cockraoch y Player

        if (distant <= 3) //Si el jugador esta a menos de 3 metros
        {
            animator.SetBool("Runcockroach", true); // Pasa al estado de huida
        }
        if (GameManager.Instance.LightFireOn == true)// Si se enciende la luz
        {
            animator.SetBool("Lights", true);// Pasa al estado de luces.
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
            return;
        
        // Set the agent to go to the currently selected destination.
       
        destPoint = (destPoint + 1) % patrolPoints.Length;//Hacemos que repita la rutina si llega al final del array, esto nos sirve para hacer un bucle y que no se quede quieto
       
        AgentCockRoach.destination = patrolPoints[destPoint].position;
    }
}
