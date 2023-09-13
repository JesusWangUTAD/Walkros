using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaveCiego : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("Josua3").GetComponent<Player>().setTextUI("Menos mal, parece que se ha ido. En la caja había un USB... para qué podrá servir");
        Destroy(animator.gameObject);
    }
    //Seria meter una cinematica de salir por la ventana en vez de esto, pero se deja de momento asi.




    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


}
