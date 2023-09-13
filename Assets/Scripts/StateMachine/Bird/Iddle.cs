using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iddle : StateMachineBehaviour
{
    private GameObject player; //El jugador
    private GameObject bird; // El pájaro
    private float distance; //La distancia

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player"); // El player es igual al tag "Player"
        bird = animator.gameObject; //Cogemos el animator del Bird
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(player.transform.position, bird.transform.position); //Distancia entre el pájaro y el jugador

        if (distance <= 2 )//Si la distancia es menor que 5 
        {
            animator.SetBool("Nervios", true); //Se activa el estado "Ruido"
        }
    }
}
