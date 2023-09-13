using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nervios : StateMachineBehaviour
{
    private GameObject player; //El jugador
    private GameObject bird; //El pájaro
    private float distance; //La distancia
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player"); // El player es igual al tag "Player"
        bird = animator.gameObject; //Cogemos el animator del Bird
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        distance = Vector3.Distance(player.transform.position, bird.transform.position); //Distancia entre el pájaro y el jugador

        if (distance >= 2)//Si la distancia es mayor que 5 
        {
            animator.SetBool("Nervios", false); //Se cancela el estado "Nervios"
        }

        if (GameObject.Find("Main Camera").GetComponent<GM_Level1>().birdCageOpen == true) //Si la jaula del pájaro se abre
        {
            animator.SetTrigger("Volar"); //El pájaro vuela
        }

    }

}
